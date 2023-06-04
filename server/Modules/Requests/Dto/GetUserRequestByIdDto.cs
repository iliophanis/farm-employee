using server.Data.Entities;
using server.Modules.Common.Extensions;

namespace server.Modules.Requests.Dto;

public record GetUserRequestByIdLocationDto(
        decimal Longitude,
        decimal Latitude,
        string DisplayName);


public record GetUserRequestByIdFarmerDto(
    int Id,
    string Name,
    string Email,
    string ContactInfo,
    decimal AvgRate,
    decimal AvgWorkPlaceRate,
    decimal AvgPaymentConsequenceRate
 );
public record GetUserRequestByIdDto(
    int Id,
    string JobType,
    string StartJobDate,
    int? EstimatedDuration,
    decimal? Price,
    decimal? StayAmount,
    decimal? TravelAmount,
    decimal? FoodAmount,
    string CultivationName,
    GetUserRequestByIdFarmerDto Farmer,
    GetUserRequestByIdLocationDto Location,
    List<string> Actions
);


public static class GetUserRequestByIdDtoExtensions
{
    public static GetUserRequestByIdDto ToGetUserRequestByIdDto(this Request request, List<string> actions) => new GetUserRequestByIdDto
    (
        Id: request.Id,
        JobType: request.JobType,
        StartJobDate: request.StartJobDate.GetLocalDateString(),
        EstimatedDuration: request.EstimatedDuration,
        Price: request.Price,
        StayAmount: request.StayAmount,
        TravelAmount: request.TravelAmount,
        FoodAmount: request.FoodAmount,
        CultivationName: request.Cultivation?.Name,
        Farmer: new GetUserRequestByIdFarmerDto
        (
            Id: request.Farmer?.Id ?? 0,
            Name: request.Farmer?.User.DisplayName,
            Email: request.Farmer.User.Email,
            ContactInfo: request.Farmer.ContactInfo.DisplayName,
            AvgRate: request.Farmer.AvgRate,
            AvgWorkPlaceRate: request.Farmer.AvgWorkPlaceRate,
            AvgPaymentConsequenceRate: request.Farmer.AvgPaymentConsequenceRate
        ),
        Location: new GetUserRequestByIdLocationDto
        (
            Longitude: request.Location?.Longitude ?? 0,
            Latitude: request.Location?.Latitude ?? 0,
            DisplayName: request.Location.DisplayName
        ),
        Actions: actions
    );
}
