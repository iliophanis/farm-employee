using FluentValidation;

namespace server.Modules.EmployeeRequests.Commands.UpdateEmployeeRequest
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequestCommand>
    {
        public UpdateEmployeeRequestValidator()
        {
        }
    }
}