namespace server.Modules.Requests.Dto
{
    public record URequestDto(string jobType, 
                             string StartJobDate, 
                             int EstimatedDuration,
                             decimal Price,
                             decimal StayAmount,
                             decimal TravelAmount,
                             decimal FoodAmount
                             );
    public record ULocationDto(decimal Longitude,
                                decimal Latitude,
                                string Prefecture,
                                string Country,
                                string Region,
                                string City,
                                string PostCode,
                                string Street
                                );

    public record UpdateRequestDto(string UserName, int RequestId, string CultivationName, URequestDto Request, ULocationDto Location);
    public record UpdateRequestResponseDto();
}
