namespace server.Modules.Requests.Dto;
public record GetUserRequestLocationDto(
        decimal Longitude,
        decimal Latitude);
public record GetUserRequestDto(int Id, GetUserRequestLocationDto Location);
