using FluentValidation;

namespace server.Modules.Users.Queries.GetToken
{
    public class GetTokenValidator : AbstractValidator<GetTokenQuery>
    {
        public GetTokenValidator()
        {
            _ = this.RuleFor(r => r.UserName).NotEmpty().WithMessage("The field UserName must not be empty.");
            _ = this.RuleFor(r => r.AuthProvider).NotEmpty().WithMessage("The field AuthProvider must not be empty.");
        }
    }
}