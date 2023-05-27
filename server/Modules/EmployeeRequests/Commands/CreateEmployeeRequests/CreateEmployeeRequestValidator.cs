using FluentValidation;

namespace server.Modules.EmployeeRequests.Commands.CreateEmployeeRequest
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequestCommand>
    {
        public CreateEmployeeRequestValidator()
        {
        }
    }
}