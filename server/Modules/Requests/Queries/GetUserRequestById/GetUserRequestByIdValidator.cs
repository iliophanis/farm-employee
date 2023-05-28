using FluentValidation;

namespace server.Modules.Requests.Queries.GetUserRequestById
{
    public class GetUserRequestByIdValidator : AbstractValidator<GetUserRequestByIdQuery>
    {
        public GetUserRequestByIdValidator()
        {
            _ = this.RuleFor(r => r.RequestId).NotNull().WithMessage("The field RequestId must not be empty.");
        }
    }
}