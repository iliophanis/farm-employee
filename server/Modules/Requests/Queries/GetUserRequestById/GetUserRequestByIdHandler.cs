using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Models;
using server.Modules.Common.Services;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequestById
{
    public class GetUserRequestByIdHandler : IRequestHandler<GetUserRequestByIdQuery, GetUserRequestByIdDto>
    {
        private readonly DataContext _context;

        private readonly CurrentUserService _currentUserService;

        public GetUserRequestByIdHandler(DataContext context, CurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<GetUserRequestByIdDto> Handle(GetUserRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var actions = new List<string>();
            var employeeId = _currentUserService.GetEmployeeId();
            if (employeeId != null)
            {
                var hasExistedOpenRequest = await _context.EmployeeRequests.AnyAsync(x => x.EmployeeId == employeeId && x.RequestId == request.RequestId, cancellationToken);
                if (!hasExistedOpenRequest) actions.Add(ApplicationPermission.Submit.ToString());
            }

            var jobRequest = await _context.Requests
            .Include(x => x.Location)
            .Include(x => x.Cultivation)
            .Include(x => x.Farmer).ThenInclude(x => x.User)
            .Include(x => x.Farmer).ThenInclude(x => x.ContactInfo)
            .Where(x => x.Id == request.RequestId)
            .Select(x => x.ToGetUserRequestByIdDto(actions))
            .FirstOrDefaultAsync(cancellationToken);

            if (jobRequest is null) throw new NotFoundException($"Request with id {request.RequestId} not found.");

            return jobRequest;
        }
    }
}