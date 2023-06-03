using server.Data.Entities;

namespace server.Modules.EmployeeRequests.Dto;

public record GetListEmployeeRequestDto(
    string FirstName,
    string LastName,
    string Email
);
