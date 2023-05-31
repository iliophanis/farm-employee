using FluentValidation;

namespace server.Modules.EmployeeRequests.Commands.SubmitRequest
{
    public class SubmitRequestValidator : AbstractValidator<SubmitRequestCommand>
    {
        public SubmitRequestValidator()
        {
        }
    }
}