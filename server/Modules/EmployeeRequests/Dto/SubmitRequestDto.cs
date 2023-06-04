namespace server.Modules.EmployeeRequests.Dto
{
    public record SubEmployeeDto(string FirstName,
                                string LastName,
                                string Email
                                );

    public record SubmitRequestDto(int RequestId,
                                   List<SubEmployeeDto> SubEmployees
                                   );
    public record SubmitRequestResponseDto();
}