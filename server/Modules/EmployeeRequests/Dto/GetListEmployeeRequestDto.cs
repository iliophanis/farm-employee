using server.Data.Entities;

// all employee requests fo a specific farmer request

namespace server.Modules.EmployeeRequests.Dto;
public record GetListEmployeeRequestResponseDto(string employeeName, string employeeLastName, string employeeEmail, bool? MessageSent, PaymentStatus PaymentStatus, PaymentMethod PaymentMethod, List<SubEmployee> SubEmployees);