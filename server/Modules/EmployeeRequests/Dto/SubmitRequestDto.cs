namespace server.Modules.EmployeeRequests.Dto
{
    public record SubEmployeeContactInfoDto(string Address,
                               string City,
                               string Tk,
                               string PhoneNo,
                               string MobilePhoneNo);

    public record SubEmployeeDto(string FirstName,
                                string LastName,
                                string Email,
                                SubEmployeeContactInfoDto ContactInfo
                                );

    public record SubmitRequestDto(int RequestId,
                                   List<SubEmployeeDto> SubEmployees
                                   );
    public record SubmitRequestResponseDto();
}