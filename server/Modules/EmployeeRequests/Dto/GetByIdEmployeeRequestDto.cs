using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto;

public record GetEmployeeInfoDto(
              string FirstName,
              string LastName,
              string Email    
            );

public record GetByIdEmployeeRequestDto(
                bool? MessageSent,
                PaymentStatus PaymentStatus,
                PaymentMethod PaymentMethod,
                GetEmployeeInfoDto Employee
            )
            {
                public List<SubEmployee> Subemployees { get; internal set; }
            }

public static class GetByIdEmployeeRequestDtoExtensions
{
    public static GetByIdEmployeeRequestDto ToGetByIdEmployeeRequestDto(this EmployeeRequest request) => new GetByIdEmployeeRequestDto
    (        
        MessageSent: request.MessageSent,
        PaymentStatus: request.PaymentStatus,
        PaymentMethod: request.PaymentMethod,  
        Employee: new GetEmployeeInfoDto(
            FirstName: request.Employee.User.FirstName,
            LastName: request.Employee.User.LastName,
            Email: request.Employee.User.Email  
        )
    );
}
