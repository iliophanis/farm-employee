using server.Data.Entities;
using server.Modules.Common.Extensions;
using server.Modules.Requests.Dto;


public record EmployeeRequestDto(
    bool? MessageSent,
    PaymentMethod? PaymentMethod,
    PaymentStatus PaymentStatus,
    int EmployeeId,
    string Name,
    string Email,
    string ContactInfo,
    decimal? AvgRate,
    decimal? AvgJobQuality,
    decimal? AvgContactQuality,
    decimal? AvgPrice,
    List<SubEmployeeRequestDto> SubEmployees
);
public record SubEmployeeRequestDto(
    string Name,
    string Email,
    string ContactInfo
);

public record GetFarmerRequestsDto(
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
    GetUserRequestByIdLocationDto Location,
    List<EmployeeRequestDto> EmployeeRequests
);

public record GetFarmerRequestsResponseDto(List<GetFarmerRequestsDto> Data, int TotalSize, int TotalPages);

public static class GetFarmerRequestsDtoExtensions
{
    public static GetFarmerRequestsDto ToGetPersonalFarmerRequestsDto(this Request request) => new GetFarmerRequestsDto
    (
        RequestId: request.Id,
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
            ContactInfo: request.Farmer.ContactInfo != null ? request.Farmer.ContactInfo.DisplayName : "-",
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
        EmployeeRequests: request.EmployeeRequests.Select(
            x => new EmployeeRequestDto(
            MessageSent: x.MessageSent,
            PaymentStatus: x.PaymentStatus,
            PaymentMethod: x.PaymentMethod,
            EmployeeId: x.Employee.Id,
            Name: x.Employee.User.DisplayName,
            Email: x.Employee.User.Email,
            ContactInfo: x.Employee.ContactInfo != null ? x.Employee.ContactInfo.DisplayName : "-",
            AvgRate: x.Employee.AvgRate,
            AvgJobQuality: x.Employee.AvgJobQuality,
            AvgPrice: x.Employee.AvgPrice,
            AvgContactQuality: x.Employee.AvgContactQuality,
            SubEmployees: x.SubEmployees.Select(y => new SubEmployeeRequestDto(
                        Name: y.DisplayName,
                        Email: y.Email,
                        ContactInfo: y.ContactInfo != null ? y.ContactInfo.DisplayName : "-"
                                                )).ToList()
            )).ToList()
    );

    public static GetFarmerRequestsDto ToGetAllFarmerRequestsDto(this Request request) => new GetFarmerRequestsDto
    (
        RequestId: request.Id,
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
            ContactInfo: request.Farmer.ContactInfo != null ? request.Farmer.ContactInfo.DisplayName : "-",
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
        EmployeeRequests: new List<EmployeeRequestDto>()
    );
}