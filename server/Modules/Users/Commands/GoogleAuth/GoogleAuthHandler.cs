using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            if (isUserWithSameEmailExisted)
            {
                //TODO REGISTER USER USING THE GOOGLE AUTHENITCATOR
                
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

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            var token = GenerateToken(displayName); 

            return await Task.FromResult(new GoogleAuthResponseDto(token, new DateTime(), displayName, usersId, usersRole));
        }

    }
}