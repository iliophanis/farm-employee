using server.Data.Entities;

namespace server.Modules.Cultivations.Dto
{
    public record GetByIdDto(int Id); 

    public record GetByIdResponseDto(List<Location> locations); 
}