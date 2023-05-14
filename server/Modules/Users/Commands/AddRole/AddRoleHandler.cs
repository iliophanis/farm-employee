using MediatR;
using server.Data.Entities;
using server.Modules.Common.Exceptions;
using server.Modules.Common.Responses;

namespace server.Modules.Users.Commands.AddRole
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public AddRoleHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddRoleDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);
            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");
            if (user.RoleId is not null) throw new BadRequestException($"User {user.Email} has already role");
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == dto.RoleId);
            if (userRole is null) throw new BadRequestException($"Role with Id {dto.RoleId} is not existed");

            _context.Attach(user);
            user.RoleId = dto.RoleId;
            user.IsActive = true;
            var contactInfo = new ContactInfo();
            if (dto.ContactInfo is not null)
            {
                contactInfo = new ContactInfo
                {
                    Address = dto.ContactInfo.Address,
                    City = dto.ContactInfo.City,
                    Tk = dto.ContactInfo.Tk,
                    PhoneNo = dto.ContactInfo.PhoneNo,
                    MobilePhoneNo = dto.ContactInfo.MobilePhoneNo
                };
                await _context.ContactInfos.AddAsync(contactInfo, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (userRole.Name == "Farmer")
            {
                var farmer = new Farmer()
                {
                    UserId = user.Id,
                    ContactInfoId = dto.ContactInfo != null ? contactInfo.Id : null,
                };
                await _context.Farmers.AddAsync(farmer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                user.Farmers.Add(farmer);
            }
            else if (userRole.Name == "Employee")
            {
                var employee = new Employee()
                {
                    UserId = user.Id,
                    ContactInfoId = dto.ContactInfo != null ? contactInfo.Id : null,
                };
                await _context.Employees.AddAsync(employee, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                user.Employees.Add(employee);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new CommandResponse<string>().WithData($"Successful add role in user {user.Email}");
        }

    }
}