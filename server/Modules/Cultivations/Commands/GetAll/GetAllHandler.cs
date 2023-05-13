using MediatR;
using server.Modules.Cultivations.Dto;

namespace server.Modules.Cultivations.Commands
{
    public class GetAllHandler : IRequestHandler<GetAllCommand, GetAllResponseDto>
    {
        private readonly DataContext _context;

        public GetAllHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<GetAllResponseDto> Handle(GetAllCommand request, CancellationToken cancellationToken)    
        {
            var dto = request.GetAllDto;

            var locations = await this._context.Locations.ToListAsync(cancellationToken);  
            
            return await Task.FromResult(new GetAllResponseDto(locations));
        }
    }
}