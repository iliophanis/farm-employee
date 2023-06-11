using FluentValidation;

namespace server.Modules.EmployeeRequests.Queries.GetAllEmployeeRequests
{
    public class GetAllEmployeeRequestsValidator : AbstractValidator<GetAllEmployeeRequestsQuery>
    {
        public GetAllEmployeeRequestsValidator()
        {
            _ = this.RuleFor(r => r.CurrentPage).NotEmpty().WithMessage("The field must not be empty.");
            _ = this.RuleFor(r => r.PageSize).NotEmpty().WithMessage("The field must not be empty.");
            _ = this.RuleFor(r => r.Type).NotEmpty().WithMessage("The field must not be empty.");
        }
    }
}