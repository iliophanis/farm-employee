using FluentValidation;

namespace server.Modules.Requests.Commands.UpdateRequest
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequestCommand>
    {
        public UpdateRequestValidator()
        {
        }
    }
}