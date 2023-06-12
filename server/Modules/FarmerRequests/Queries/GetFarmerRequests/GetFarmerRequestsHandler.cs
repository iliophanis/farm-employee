using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Services;

namespace server.Modules.EmployeeRequests.Queries.GetFarmerRequests
{
    public class GetFarmerRequestsHandler : IRequestHandler<GetFarmerRequestsQuery, GetFarmerRequestsResponseDto>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        private readonly CurrentUserService _currentUserService;

        public GetFarmerRequestsHandler(DataContext context, IConfiguration configuration, CurrentUserService currentUserService)
        {
            _context = context;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }

        public async Task<GetFarmerRequestsResponseDto> Handle(GetFarmerRequestsQuery request, CancellationToken cancellationToken)
        {
            if (request.Type != "all" && request.Type != "personal") throw new BadRequestException("Invalid Type value");

            var farmerId = _currentUserService.GetFarmerId();

            var totalSize = 0;
            List<GetFarmerRequestsDto> data = new();
            if (request.Type == "all")
            {
                var requestsQueryable = _context.Requests.OrderByDescending(m => m.UpdateDate).AsQueryable();
                if (!string.IsNullOrEmpty(request.Filter))
                {
                    requestsQueryable = requestsQueryable.Where(
                        x => x.Location.City.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.JobType.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Cultivation.Name.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Farmer.User.Email.ToUpper().Contains(request.Filter.ToUpper()));
                }

                data = await requestsQueryable
                        .Include(x => x.Location)
                        .Include(x => x.Cultivation)
                        .Include(x => x.Farmer).ThenInclude(x => x.User)
                        .Include(x => x.Farmer).ThenInclude(x => x.ContactInfo)
                        .Skip((request.CurrentPage - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(x => x.ToGetAllFarmerRequestsDto())
                        .ToListAsync(cancellationToken);

                totalSize = await requestsQueryable.CountAsync(cancellationToken);
            }
            else
            {
                var queryable = _context.Requests.Where(x => x.FarmerId == farmerId).OrderByDescending(m => m.UpdateDate).AsQueryable();
                if (!string.IsNullOrEmpty(request.Filter))
                {
                    queryable = queryable.Where(
                        x => x.Location.City.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.JobType.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Cultivation.Name.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Farmer.User.Email.ToUpper().Contains(request.Filter.ToUpper()));
                }

                data = await queryable
                        .Include(x => x.Location)
                        .Include(x => x.Cultivation)
                        .Include(x => x.Farmer).ThenInclude(x => x.User)
                        .Include(x => x.Farmer).ThenInclude(x => x.ContactInfo)
                        .Include(x => x.EmployeeRequests).ThenInclude(x => x.Employee).ThenInclude(x => x.User)
                        .Include(x => x.EmployeeRequests).ThenInclude(x => x.Employee).ThenInclude(x => x.ContactInfo)
                        .Include(x => x.EmployeeRequests).ThenInclude(x => x.SubEmployees).ThenInclude(x => x.ContactInfo)
                        .Skip((request.CurrentPage - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(x => x.ToGetPersonalFarmerRequestsDto())
                        .ToListAsync(cancellationToken);


                totalSize = await queryable.CountAsync(cancellationToken);
            }



            var totalPages = 0;
            if (request.PageSize != 0)
            {
                totalPages = (int)Math.Ceiling((totalSize / (double)request.PageSize));
            }

            return new GetFarmerRequestsResponseDto(data, totalSize, totalPages);

        }
    }
}