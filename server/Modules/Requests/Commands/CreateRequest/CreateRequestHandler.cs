using MediatR;
using server.Modules.Common.Exceptions;
using server.Modules.Requests.Dto;
using server.Data.Entities;

namespace server.Modules.Requests.Commands.CreateRequest
{
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, CreateRequestResponseDto>
    {
        private readonly DataContext _context;

        public CreateRequestHandler(DataContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<CreateRequestResponseDto> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateRequestDto;
            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");
            if (user.Role.Name is not "Farmer") throw new BadRequestException($"User {user.Email} is not a farmer.");
            if (dto.Request is null || dto.Location is null) throw new NotFoundException("All fields should be filled.");

            var location = new Location{
                Longitude = dto.Location.Longitude,
                Latitude = dto.Location.Latitude,
                Prefecture = dto.Location.Prefecture,
                Country = dto.Location.Country,
                Region = dto.Location.Region,
                City = dto.Location.City,
                PostCode = dto.Location.PostCode,
                Street = dto.Location.Street
            };

            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var farmer = await _context.Farmers
                    .Where(f => f.UserId == user.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            var cultivation = new Cultivation{
                Name = dto.CultivationName
            };

            await _context.Cultivations.AddAsync(cultivation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var requests = new Request{
                JobType = dto.Request.jobType,
                StartJobDate = System.DateOnly.Parse(dto.Request.StartJobDate),
                EstimatedDuration = dto.Request.EstimatedDuration,
                Price = dto.Request.EstimatedDuration,
                StayAmount = dto.Request.StayAmount,
                TravelAmount = dto.Request.TravelAmount,
                FoodAmount = dto.Request.FoodAmount,
                LocationId = location.Id, 
                FarmerId = farmer.Id,    
                CultivationId = cultivation.Id 
            };
            
            await _context.Requests.AddAsync(requests, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(new CreateRequestResponseDto());

        }

    }

}