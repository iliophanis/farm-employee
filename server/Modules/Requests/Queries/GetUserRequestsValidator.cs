using FluentValidation;

namespace server.Modules.Requests.Queries.GetUserRequests
{
    public class GetUserRequestsValidator : AbstractValidator<GetUserRequestsQuery>
    {
        public GetUserRequestsValidator()
        {
        }
    }
}