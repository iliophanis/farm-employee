using MediatR;
using server.Modules.Common.Exceptions;
using server.Data.Entities;
using server.Modules.Common.Responses;

namespace server.Modules.Requests.Commands.CreateRequest
{
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public CreateRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateRequestDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var location = new Location
            {
                Longitude = dto.Location.Longitude,
                Latitude = dto.Location.Latitude,
                Prefecture = dto.Location.Prefecture,
                Country = dto.Location.Country,
                Region = dto.Location.Region,
                City = dto.Location.City,
                PostCode = dto.Location.PostCode,
                Street = dto.Location.Street
            };

            var farmerId = await _context.Farmers
                            .Where(f => f.UserId == user.Id)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync(cancellationToken);

            if (farmerId == 0) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var cultivation = new Cultivation
            {
                Name = dto.CultivationName
            };

            var farmerRequest = new Request
            {
                JobType = dto.Request.jobType,
                StartJobDate = System.DateTime.Parse(dto.Request.StartJobDate),
                EstimatedDuration = dto.Request.EstimatedDuration,
                Price = dto.Request.EstimatedDuration,
                StayAmount = dto.Request.StayAmount,
                TravelAmount = dto.Request.TravelAmount,
                FoodAmount = dto.Request.FoodAmount,
                FarmerId = farmerId,
                Cultivation = cultivation,
                Location = location
            };

            await _context.Requests.AddAsync(farmerRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData($"Successful request insertion with Id {farmerRequest.Id}");

        }

    }

}