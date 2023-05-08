using server.Data.Entities;

namespace server.Modules.Cultivations.Dto
{
    public record GetAllDto();

    public record GetAllResponseDto(List<Location> locations); 
}