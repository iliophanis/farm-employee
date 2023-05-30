using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Queries.GetUserRequestById
{
    public class GetUserRequestByIdHandler : IRequestHandler<GetUserRequestByIdQuery, GetUserRequestByIdDto>
    {
        private readonly DataContext _context;

        public GetUserRequestByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetUserRequestByIdDto> Handle(GetUserRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var jobRequest = await _context.Requests
            .Include(x => x.Location)
            .Include(x => x.Cultivation)
            .Include(x => x.Farmer)
            .ThenInclude(x => x.User)
            .Where(x => x.Id == request.RequestId)
            .Select(x => x.ToGetUserRequestByIdDto())
            .FirstOrDefaultAsync(cancellationToken);

            if (jobRequest is null) throw new NotFoundException($"Request with id {request.RequestId} not found.");
            return jobRequest;
        }
    }
}