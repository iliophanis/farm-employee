using FluentValidation;

namespace server.Modules.EmployeeRequests.Queries.GetByIdEmployeeRequest
{
    public class GetByIdEmployeeRequestValidator : AbstractValidator<GetByIdEmployeeRequestQuery>
    {
        public GetByIdEmployeeRequestValidator()
        {
            _ = this.RuleFor(r => r.employeeRequestId).NotEmpty().WithMessage("The field must not be empty.");
        }
    }
}