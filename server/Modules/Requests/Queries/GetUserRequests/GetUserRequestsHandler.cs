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
            var requests = _context.Requests
            .Include(x => x.Location)
            .AsQueryable();

            if (request.MinLat is not null &&
                request.MaxLat is not null &&
                request.MinLon is not null &&
                request.MaxLon is not null)
            {
                requests = requests.Where(d =>
                    d.Location.Longitude >= request.MinLon &&
                    d.Location.Longitude <= request.MaxLon &&
                    d.Location.Latitude >= request.MinLat &&
                    d.Location.Latitude <= request.MaxLat);
            }

            var requestsList = await requests
            .OrderByDescending(x => x.Id)
            .Take(10)
            .Select(x => new GetUserRequestDto(x.Id, new GetUserRequestLocationDto(x.Location.Longitude, x.Location.Latitude)))
            .ToListAsync(cancellationToken);

            return requestsList;
        }
    }
}