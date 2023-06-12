using server.Data.Entities;
using server.Modules.Common.Extensions;
using server.Modules.Requests.Dto;

public record GetAllEmployeeRequestsDto(
    int? EmployeeRequestId,
    int RequestId,
    string JobType,
    string StartJobDate,
    int? EstimatedDuration,
    decimal? Price,
    decimal? StayAmount,
    decimal? TravelAmount,
    decimal? FoodAmount,
    string CultivationName,
    GetUserRequestByIdFarmerDto Farmer,
    GetUserRequestByIdLocationDto Location
);

public record GetAllEmployeeRequestsResponseDto(List<GetAllEmployeeRequestsDto> Data, int TotalSize, int TotalPages);

public static class GetAllEmployeeRequestsDtoExtensions
{
    public static GetAllEmployeeRequestsDto ToGetAllEmployeeRequestsDto(this EmployeeRequest empRequest) => new GetAllEmployeeRequestsDto
    (
        EmployeeRequestId: empRequest.Id,
        RequestId: empRequest.Request.Id,
        JobType: empRequest.Request.JobType,
        StartJobDate: empRequest.Request.StartJobDate.GetLocalDateString(),
        EstimatedDuration: empRequest.Request.EstimatedDuration,
        Price: empRequest.Request.Price,
        StayAmount: empRequest.Request.StayAmount,
        TravelAmount: empRequest.Request.TravelAmount,
        FoodAmount: empRequest.Request.FoodAmount,
        CultivationName: empRequest.Request.Cultivation?.Name,
        Farmer: new GetUserRequestByIdFarmerDto
        (
            Id: empRequest.Request.Farmer?.Id ?? 0,
            Name: empRequest.Request.Farmer?.User.DisplayName,
            Email: empRequest.Request.Farmer.User.Email,
            ContactInfo: empRequest.Request.Farmer.ContactInfo != null ? empRequest.Request.Farmer.ContactInfo.DisplayName : "-",
            AvgRate: empRequest.Request.Farmer.AvgRate,
            AvgWorkPlaceRate: empRequest.Request.Farmer.AvgWorkPlaceRate,
            AvgPaymentConsequenceRate: empRequest.Request.Farmer.AvgPaymentConsequenceRate
        ),
        Location: new GetUserRequestByIdLocationDto
        (
            Longitude: empRequest.Request.Location?.Longitude ?? 0,
            Latitude: empRequest.Request.Location?.Latitude ?? 0,
            DisplayName: empRequest.Request.Location.DisplayName
        )
    );

    public static GetAllEmployeeRequestsDto ToGetAllRequestsDto(this Request empRequest) => new GetAllEmployeeRequestsDto
    (
        EmployeeRequestId: null,
        RequestId: empRequest.Id,
        JobType: empRequest.JobType,
        StartJobDate: empRequest.StartJobDate.GetLocalDateString(),
        EstimatedDuration: empRequest.EstimatedDuration,
        Price: empRequest.Price,
        StayAmount: empRequest.StayAmount,
        TravelAmount: empRequest.TravelAmount,
        FoodAmount: empRequest.FoodAmount,
        CultivationName: empRequest.Cultivation?.Name,
        Farmer: new GetUserRequestByIdFarmerDto
        (
            Id: empRequest.Farmer?.Id ?? 0,
            Name: empRequest.Farmer?.User.DisplayName,
            Email: empRequest.Farmer.User.Email,
            ContactInfo: empRequest.Farmer.ContactInfo != null ? empRequest.Farmer.ContactInfo.DisplayName : "-",
            AvgRate: empRequest.Farmer.AvgRate,
            AvgWorkPlaceRate: empRequest.Farmer.AvgWorkPlaceRate,
            AvgPaymentConsequenceRate: empRequest.Farmer.AvgPaymentConsequenceRate
        ),
        Location: new GetUserRequestByIdLocationDto
        (
            Longitude: empRequest.Location?.Longitude ?? 0,
            Latitude: empRequest.Location?.Latitude ?? 0,
            DisplayName: empRequest.Location.DisplayName
        )
    );
}