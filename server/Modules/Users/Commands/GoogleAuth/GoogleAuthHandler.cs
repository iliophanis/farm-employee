using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using server.Modules.Users.Dto;

namespace server.Modules.Users.Commands.GoogleAuth
{
    public class GoogleAuthHandler : IRequestHandler<GoogleAuthCommand, GoogleAuthResponseDto>
    {
        private readonly DataContext _context;

        public GoogleAuthHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GoogleAuthResponseDto> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
        {
            var dto = request.GoogleAuthDto;
            var isUserWithSameEmailExisted = await _context.Users.AnyAsync(u => u.Email == dto.UserName);
            if (!isUserWithSameEmailExisted)
            {
                //TODO REGISTER USER USING THE GOOGLE AUTHENITCATOR
                var newUser = new Data.Entities.User { 
                    Email = request.GoogleAuthDto.UserName,
                    FirstName = request.GoogleAuthDto.FirstName,
                    LastName = request.GoogleAuthDto.LastName
                    };
                    
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);                
            }
            //FIND THE RECORD IN DB AND SENT HIM THE TOKEN, EXPIRATION AND CLAIMS LIKE EMAIL
            //TODO BUSINESS LOGIC
            var displayName = await _context.Users
                        .Where(u => u.Email == dto.UserName)
                        .Select(u => u.FirstName)
                        .FirstOrDefaultAsync();     

            var usersId = await _context.Users
                        .Where(u => u.Email == dto.UserName)
                        .Select(u => u.Id)
                        .FirstOrDefaultAsync();   

            var usersRole = await _context.Users
                        .Where(u => u.Email == dto.UserName)
                        .Select(u => u.Role)
                        .FirstOrDefaultAsync(); 

            var usersEmail = await _context.Users
                        .Where(u => u.Email == dto.UserName)
                        .Select(u => u.Email)
                        .FirstOrDefaultAsync();

            var roles = _context.Users
                    .Where(u=>u.Email == dto.UserName)
                    .Select(u=>u.Role)
                    .ToList(); 

            var claims = new[] { new Claim(ClaimTypes.Name, usersEmail) };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key")), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("farm-employees", "farm-employees", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);           
            
            return await Task.FromResult(new GoogleAuthResponseDto(tokenValue, new DateTime(), displayName, usersId, roles));
        }

    }
}