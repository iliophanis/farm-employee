using FluentValidation;

namespace server.Modules.Users.Commands.GoogleAuth
{
    public class GoogleAuthValidator : AbstractValidator<GoogleAuthCommand>
    {
        public GoogleAuthValidator()
        {
            _ = this.RuleFor(r => r.GoogleAuthDto.FirstName).NotNull().WithMessage("The field FirstName must not be empty.");
            _ = this.RuleFor(r => r.GoogleAuthDto.LastName).NotNull().WithMessage("The field LastName must not be empty.");
            _ = this.RuleFor(r => r.GoogleAuthDto.UserName).NotEmpty().WithMessage("The field UserName must not be empty.");
        }
    }
}