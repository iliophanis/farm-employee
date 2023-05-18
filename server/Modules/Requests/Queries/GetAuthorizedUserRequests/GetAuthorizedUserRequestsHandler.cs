using MediatR;
using server.Data.Entities;

namespace server.Modules.Requests.Queries.GetAuthorizedUserRequests
{
    public class GetAuthorizedUserRequestsHandler : IRequestHandler<GetAuthorizedUserRequestsQuery, List<Request>>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public GetAuthorizedUserRequestsHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Request>> Handle(GetAuthorizedUserRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
            .OrderByDescending(x => x.Id)
            .Take(20)
            .Include(x => x.Location)
            .Include(x => x.Farmer)
            .Include(x => x.Cultivation)
            .ToListAsync();

            return requests;
        }
    }
}