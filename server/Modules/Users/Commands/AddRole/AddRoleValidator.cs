using FluentValidation;

namespace server.Modules.Users.Commands.AddRole
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator()
        {
            _ = this.RuleFor(r => r.AddRoleDto.UserName).NotEmpty().WithMessage("The field FirstName must not be empty.");
            _ = this.RuleFor(r => r.AddRoleDto.RoleId).NotEmpty().WithMessage("The field LastName must not be empty.");
            _ = this.RuleFor(r => r.AddRoleDto.UserName).NotEmpty().WithMessage("The field UserName must not be empty.");
        }
    }
}