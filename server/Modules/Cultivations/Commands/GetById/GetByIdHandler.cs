using MediatR;
using server.Modules.Cultivations.Dto;

namespace server.Modules.Cultivations.Commands
{
    public class GetByIdHandler : IRequestHandler<GetByIdCommand, GetByIdResponseDto>
    {
        private readonly DataContext _context;

        public GetByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetByIdResponseDto> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            var dto = request.GetByIdDto;
            var location = await this._context.Locations.Where(l => l.Id == dto.Id).ToListAsync(); 
            return await Task.FromResult(new GetByIdResponseDto(location));  
        }

    }
}