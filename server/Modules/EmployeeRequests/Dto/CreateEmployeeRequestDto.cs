using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto
{    
    public record EmployeeRequestDto(bool MessageSent,
                                    PaymentMethod PaymentMethod,
                                    PaymentStatus PaymentStatus,
                                    int EmployeeId,
                                    int RequestId,
                                    int PackageId
                                    );
    public record SubEmployeeDto(string FirstName,
                                string LastName,
                                string Email
                                );
    public record PackageDto(string Name,
                            decimal Price,
                            decimal Discount,
                            int MaxRequests
                            );
    public record CreateEmployeeRequestDto(string UserName, int requestId, PackageDto Package, List<SubEmployeeDto> SubEmployee, EmployeeRequestDto EmployeeRequest);
    public record CreateRequestEmployeeResponseDto();
}