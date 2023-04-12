using MediatR;
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
            return await Task.FromResult(new GoogleAuthResponseDto(null, new DateTime(), null, -1, null));
        }
    }
}