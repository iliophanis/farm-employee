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

        public Task<GetByIdResponseDto> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            //var dto = request.GetByIdDto;
            var id = request.id;   //TODO: check this!!!!!!!!
            var location = this._context.Locations.Where(l => l.Id == id).ToListAsync(cancellationToken).Result; 
            return Task.FromResult(new GetByIdResponseDto(location));  
        }
    }
}