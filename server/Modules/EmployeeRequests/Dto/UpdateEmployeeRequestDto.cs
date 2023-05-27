using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto
{
    public record UEmployeeRequestDto(bool MessageSent,
                                    PaymentMethod PaymentMethod,
                                    PaymentStatus PaymentStatus,
                                    int EmployeeId,
                                    int RequestId,
                                    int PackageId
                                    );
    public record USubEmployeeDto(string FirstName,
                                string LastName,
                                string Email
                                );
    public record UPackageDto(string Name,
                            decimal Price,
                            decimal Discount,
                            int MaxRequests
                            );
    public record UpdateEmployeeRequestDto(string UserName, int RequestId, UEmployeeRequestDto EmployeeRequest, List<USubEmployeeDto> SubEmployee, UPackageDto Package);
    public record UpdateEmployeeRequestResponseDto();
}