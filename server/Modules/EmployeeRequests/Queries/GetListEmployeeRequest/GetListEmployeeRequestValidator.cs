using FluentValidation;

namespace server.Modules.EmployeeRequests.Queries.GetListEmployeeRequest
{
    public class GetListEmployeeRequestValidator : AbstractValidator<GetListEmployeeRequestQuery>
    {
        public GetListEmployeeRequestValidator()
        {
            _ = this.RuleFor(r => r.requestId).NotEmpty().WithMessage("The field must not be empty.");
        }
    }
}