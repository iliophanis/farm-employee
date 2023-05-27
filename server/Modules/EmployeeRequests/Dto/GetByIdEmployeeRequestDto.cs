using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto;
public record GetByIdEmployeeRequestResponseDto(string employeeName, string employeeLastName, string employeeEmail, bool? MessageSent, PaymentStatus PaymentStatus, PaymentMethod PaymentMethod, List<SubEmployee> SubEmployees);

// request of a specific employee for a specific farmer request