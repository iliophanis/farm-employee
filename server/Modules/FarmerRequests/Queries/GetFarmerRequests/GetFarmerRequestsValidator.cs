using FluentValidation;

namespace server.Modules.EmployeeRequests.Queries.GetFarmerRequests
{
    public class GetFarmerRequestsValidator : AbstractValidator<GetFarmerRequestsQuery>
    {
        public GetFarmerRequestsValidator()
        {
            _ = this.RuleFor(r => r.CurrentPage).NotEmpty().WithMessage("The field must not be empty.");
            _ = this.RuleFor(r => r.PageSize).NotEmpty().WithMessage("The field must not be empty.");
            _ = this.RuleFor(r => r.Type).NotEmpty().WithMessage("The field must not be empty.");
        }
    }
}