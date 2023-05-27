using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto
{    
    public record DeleteEmployeeRequestDto(string UserName, int EmployeeRequestId);
    public record DeleteRequestEmployeeResponseDto();
}