using FluentValidation;

namespace server.Modules.Requests.Commands.CreateRequest
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestValidator()
        {
            _ = this.RuleFor(r => r.CreateRequestDto.Request.jobType).NotEmpty().WithMessage("The field Job Type must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Request.StartJobDate).NotEmpty().WithMessage("The field Start Job Date must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Request.EstimatedDuration).NotEmpty().WithMessage("The field Estimated Duration must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Request.Price).NotEmpty().WithMessage("The field price must not be empty.");    
            _ = this.RuleFor(r => r.CreateRequestDto.Request.StayAmount).NotEmpty().WithMessage("The field Stay Amount must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Request.TravelAmount).NotEmpty().WithMessage("The field Travel Amount must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Request.FoodAmount).NotEmpty().WithMessage("The field Food Amount must not be empty.");
        
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Longitude).NotEmpty().WithMessage("The field Longitude must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Latitude).NotEmpty().WithMessage("The field Latitude must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Prefecture).NotEmpty().WithMessage("The field Prefecture must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Country).NotEmpty().WithMessage("The field Country must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Region).NotEmpty().WithMessage("The field Region must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.PostCode).NotEmpty().WithMessage("The field Post Code must not be empty.");
            _ = this.RuleFor(r => r.CreateRequestDto.Location.Street).NotEmpty().WithMessage("The field Street must not be empty.");
        }
    }
}