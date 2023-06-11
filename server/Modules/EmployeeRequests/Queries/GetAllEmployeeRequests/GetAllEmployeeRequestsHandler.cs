using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Services;

namespace server.Modules.EmployeeRequests.Queries.GetAllEmployeeRequests
{
    public class GetAllEmployeeRequestsHandler : IRequestHandler<GetAllEmployeeRequestsQuery, GetAllEmployeeRequestsResponseDto>
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        private readonly CurrentUserService _currentUserService;

        public GetAllEmployeeRequestsHandler(DataContext context, IConfiguration configuration, CurrentUserService currentUserService)
        {
            _context = context;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }

        public async Task<GetAllEmployeeRequestsResponseDto> Handle(GetAllEmployeeRequestsQuery request, CancellationToken cancellationToken)
        {
            if (request.Type != "all" && request.Type != "personal") throw new BadRequestException("Invalid Type value");

            var employeeId = _currentUserService.GetEmployeeId();

            var totalSize = 0;
            List<GetAllEmployeeRequestsDto> data = new();
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
                        .Select(x => x.ToGetAllRequestsDto())
                        .ToListAsync(cancellationToken);

                totalSize = await requestsQueryable.CountAsync(cancellationToken);
            }
            else
            {
                var queryable = _context.EmployeeRequests.Where(x => x.EmployeeId == employeeId).OrderByDescending(m => m.UpdateDate).AsQueryable();
                if (!string.IsNullOrEmpty(request.Filter))
                {
                    queryable = queryable.Where(
                        x => x.Request.Location.City.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Request.JobType.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Request.Cultivation.Name.ToUpper().Contains(request.Filter.ToUpper()) ||
                        x.Request.Farmer.User.Email.ToUpper().Contains(request.Filter.ToUpper()));
                }
                data = await queryable
                        .Include(x => x.Employee).ThenInclude(x => x.User)
                        .Include(x => x.Request).ThenInclude(x => x.Location)
                        .Include(x => x.Request).ThenInclude(x => x.Cultivation)
                        .Include(x => x.Request).ThenInclude(x => x.Farmer).ThenInclude(x => x.User)
                        .Include(x => x.Request).ThenInclude(x => x.Farmer).ThenInclude(x => x.ContactInfo)
                        .Skip((request.CurrentPage - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(x => x.ToGetAllEmployeeRequestsDto())
                        .ToListAsync(cancellationToken);

                totalSize = await queryable.CountAsync(cancellationToken);
            }



            var totalPages = 0;
            if (request.PageSize != 0)
            {
                totalPages = (int)Math.Ceiling((totalSize / (double)request.PageSize));
            }

            return new GetAllEmployeeRequestsResponseDto(data, totalSize, totalPages);

        }
    }
}