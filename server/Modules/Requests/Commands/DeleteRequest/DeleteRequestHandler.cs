using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Requests.Dto;

namespace server.Modules.Requests.Commands.DeleteRequest
{
    public class DeleteRequestHandler : IRequestHandler<DeleteRequestCommand, DeleteRequestResponseDto>
    {
        private readonly DataContext _context;

        public DeleteRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<DeleteRequestResponseDto> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DeleteRequestDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var farmer = await _context.Farmers
                            .Where(f => f.UserId == user.Id)
                            .FirstOrDefaultAsync(cancellationToken);

            var req = await _context.Requests
                        .Where(r => r.Id == dto.RequestId)
                        .FirstOrDefaultAsync(cancellationToken);
            if (req is null) throw new NotFoundException("Request not found");
            if (req.FarmerId != farmer.Id) throw new BadRequestException("Changes are allowed only from owner");

            if (req is not null)
            {
                var cultivation = await _context.Cultivations
                                    .Where(c => c.Id == req.CultivationId)
                                    .FirstOrDefaultAsync(cancellationToken);

                var location = await _context.Locations
                                    .Where(l => l.Id == req.LocationId)
                                    .FirstOrDefaultAsync(cancellationToken);

                _context.Cultivations.Remove(cultivation);
                _context.Locations.Remove(location);
                _context.Requests.Remove(req);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Task.FromResult(new DeleteRequestResponseDto());
        }
    }
}