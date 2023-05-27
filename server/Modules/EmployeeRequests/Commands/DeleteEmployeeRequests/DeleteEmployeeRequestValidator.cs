using FluentValidation;

namespace server.Modules.EmployeeRequests.Commands.DeleteEmployeeRequest
{
    public class DeleteEmployeeRequestValidator : AbstractValidator<DeleteEmployeeRequestCommand>
    {
        public DeleteEmployeeRequestValidator()
        {
        }
    }
}