namespace server.Modules.Requests.Dto
{    
    public record RequestDto(string jobType, 
                             DateOnly StartJobDate, 
                             int EstimatedDuration,
                             decimal Price,
                             decimal StayAmount,
                             decimal TravelAmount,
                             decimal FoodAmount,
                             int FarmerId,
                             int CultivationId
                             );
    public record LocationDto(decimal Longitude,
                                decimal Latitude,
                                string Prefecture,
                                string Country,
                                string Region,
                                string City,
                                string PostCode,
                                string Street);
    public record CreateRequestDto(int RoleId, string UserName, RequestDto Request, LocationDto Location, string CultivationName);
    public record CreateRequestResponseDto();
}
