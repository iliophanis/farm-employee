using MediatR;
using server.Data.Entities;
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
            var user = await _context.Users
            .Include(x => x.Role)
            .Where(x => x.Email == dto.UserName)
            .FirstOrDefaultAsync(cancellationToken);
            if (user != null)
            {
                var newUser = new User
                {
                    Email = request.GoogleAuthDto.UserName,
                    FirstName = request.GoogleAuthDto.FirstName,
                    LastName = request.GoogleAuthDto.LastName,
                    AuthProvider = AuthProvider.Google,
                    Password = "",
                    EmailConformed = true,
                    IsActive = false
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);
                return new GoogleAuthResponseDto(true);
            }

            return new GoogleAuthResponseDto(user.RoleId == null);
        }

    }
}