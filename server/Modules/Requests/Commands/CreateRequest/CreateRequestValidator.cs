using FluentValidation;

namespace server.Modules.Requests.Commands.CreateRequest
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestValidator()
        {
        }
    }
}