using FluentValidation;

namespace server.Modules.Requests.Commands.DeleteRequest
{
    public class DeleteRequestValidator : AbstractValidator<DeleteRequestCommand>
    {
        public DeleteRequestValidator()
        {
        }
    }
}