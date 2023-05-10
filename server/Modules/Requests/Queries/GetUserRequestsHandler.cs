using MediatR;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequests
{
    public class GetUserRequestsHandler : IRequestHandler<GetUserRequestsQuery, List<GetUserRequestDto>>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public GetUserRequestsHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<GetUserRequestDto>> Handle(GetUserRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
            .Take(10)
            .Include(x => x.Location)
            .Select(x => new GetUserRequestDto(x.Location.Longtitude, x.Location.Latitude))
            .ToListAsync();

            return requests;
        }
    }
}