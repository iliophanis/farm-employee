using MediatR;
using server.Modules.Cultivations.Dto;

namespace server.Modules.Cultivations.Commands
{
    class GetByIdHandler : IRequestHandler<GetByIdCommand, GetByIdResponseDto>
    {
        private readonly DataContext _context;

        private GetByIdHandler(DataContext context)
        {
        _context = context;
        }

        public async Task<GetByIdResponseDto> Handle(GetByIdCommand request, CancellationToken token)
        {
            var dto = request.GetByIdDto;
            var location = await this._context.Locations.Where(l => l.Id == dto.Id).ToListAsync();
            return await Task.FromResult(new GetByIdResponseDto(location));
        }

    }
}