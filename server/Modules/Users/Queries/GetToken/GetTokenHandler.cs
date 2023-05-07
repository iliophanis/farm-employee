using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using server.Data.Entities;
using server.Modules.Common.Exceptions;
using server.Modules.Users.Dto;

namespace server.Modules.Users.Queries.GetToken
{
    public class GetTokenHandler : IRequestHandler<GetTokenQuery, GetTokenResponseDto>
    {
        private readonly DataContext _context;

        public GetTokenHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetTokenResponseDto> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            if (request.AuthProvider != "Google" && request.AuthProvider != "Facebook") throw new BadRequestException("Wrong AuthProvider property");
            var authProvider = request.AuthProvider == "Google" ? AuthProvider.Google : AuthProvider.Facebook;
            var user = await _context.Users
            .Include(x => x.Role)
            .Where(x => x.Email == request.UserName && x.AuthProvider == authProvider)
            .FirstOrDefaultAsync(cancellationToken);
            if (user is null) throw new NotFoundException($"User with userName {request.UserName} not found.");
            if (user.IsActive == false) throw new NotFoundException($"User {user.Email} is not active.");
            if (user.RoleId == null) throw new BadRequestException($"User {user.Email} not having a role");

            var claims = new[] {
                new Claim("userName", user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("id",user.Id.ToString()) };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key")), SecurityAlgorithms.HmacSha256);
            var expiresDate = DateTime.Now.AddHours(8);
            var token = new JwtSecurityToken("farm-employee", "farm-employee", claims, expires: DateTime.Now.AddHours(8), signingCredentials: credentials);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            var displayName = !String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.LastName) ? $"{user.FirstName} {user.LastName}" : user.FirstName;
            return new GetTokenResponseDto(tokenValue, expiresDate, displayName, user.Id, user.Role.Name);
        }
    }
}