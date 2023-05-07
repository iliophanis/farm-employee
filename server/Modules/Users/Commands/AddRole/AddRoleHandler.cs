using MediatR;
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
            if (user.RoleId != null) throw new BadRequestException($"User {user.Email} has already role");
            var isRoleExisted = await _context.Roles.AnyAsync(x => x.Id == dto.RoleId);
            if (!isRoleExisted) throw new BadRequestException($"Role with Id {dto.RoleId} is not existed");

            _context.Attach(user);
            user.RoleId = dto.RoleId;
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData($"Successful add role in user {user.Email}");
        }

    }
}