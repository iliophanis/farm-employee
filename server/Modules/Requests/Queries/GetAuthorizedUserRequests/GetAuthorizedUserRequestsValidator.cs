using FluentValidation;

namespace server.Modules.Requests.Queries.GetAuthorizedUserRequests
{
    public class GetAuthorizedUserRequestsValidator : AbstractValidator<GetAuthorizedUserRequestsQuery>
    {
        public GetAuthorizedUserRequestsValidator()
        {
        }
    }
}