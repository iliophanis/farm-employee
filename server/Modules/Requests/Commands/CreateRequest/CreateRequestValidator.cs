using FluentValidation;
using server.Modules.Requests.Commands.CreateRequest;

namespace server.Modules.Requests.Commands.CreateRequests
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestValidator()
        {
        }
    }
}