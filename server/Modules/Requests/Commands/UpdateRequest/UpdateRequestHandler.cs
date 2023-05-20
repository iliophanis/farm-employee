using MediatR;
using server.Modules.Common.Exceptions;
using server.Data.Entities;
using server.Modules.Common.Responses;

namespace server.Modules.Requests.Commands.UpdateRequest
{
     public class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand, CommandResponse<string>>
    {
        private readonly DataContext _context;

        public UpdateRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<string>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateRequestDto;

            var user = await _context.Users
                .Where(u => u.Email == dto.UserName)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var farmerId = await _context.Farmers
                            .Where(f => f.UserId == user.Id)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync(cancellationToken);

            if (farmerId == 0) throw new NotFoundException($"User with userName {dto.UserName} not found.");

            var updateRequest = await _context.Requests
                    .Where(r => r.Id == dto.RequestId)
                    .Include(l => l.Location)
                    .Include(c => c.Cultivation)
                    .FirstOrDefaultAsync(cancellationToken);

            if (updateRequest is null) throw new NotFoundException($"Request with id {dto.RequestId} not found");

            _context.Attach(updateRequest);

            updateRequest.Location.Longitude = dto.Location.Longitude.Equals(0) ? updateRequest.Location.Longitude : dto.Location.Longitude;
            updateRequest.Location.Latitude = dto.Location.Latitude.Equals(0) ? updateRequest.Location.Latitude : dto.Location.Latitude;
            updateRequest.Location.Prefecture = dto.Location.Prefecture.Equals("string") ? updateRequest.Location.Prefecture  : dto.Location.Prefecture;
            updateRequest.Location.Country = dto.Location.Country.Equals("string") ? updateRequest.Location.Country : dto.Location.Country;
            updateRequest.Location.Region = dto.Location.Region.Equals("string") ? updateRequest.Location.Region : dto.Location.Region;
            updateRequest.Location.City = dto.Location.City.Equals("string") ? updateRequest.Location.City : dto.Location.City;
            updateRequest.Location.PostCode = dto.Location.PostCode.Equals("string") ? updateRequest.Location.PostCode : dto.Location.PostCode;
            updateRequest.Location.Street = dto.Location.Street.Equals("string") ? updateRequest.Location.Street : dto.Location.Street;

            updateRequest.Cultivation.Name = dto.CultivationName.Equals("string") ? updateRequest.Cultivation.Name : dto.CultivationName;

            updateRequest.JobType = dto.Request.jobType.Equals("string") ? updateRequest.JobType : dto.Request.jobType;
            updateRequest.StartJobDate = dto.Request.StartJobDate.Equals(null) ? updateRequest.StartJobDate : System.DateOnly.Parse(dto.Request.StartJobDate);
            updateRequest.EstimatedDuration = dto.Request.EstimatedDuration.Equals(0) ? updateRequest.EstimatedDuration : dto.Request.EstimatedDuration;
            updateRequest.Price = dto.Request.Price.Equals(0) ? updateRequest.Price : dto.Request.Price;
            updateRequest.StayAmount = dto.Request.StayAmount.Equals(0) ? dto.Request.StayAmount : updateRequest.StayAmount;
            updateRequest.FoodAmount = dto.Request.FoodAmount.Equals(0) ? updateRequest.FoodAmount : dto.Request.FoodAmount;

            _context.Requests.Update(updateRequest);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResponse<string>().WithData($"Successful request update with Id {dto.RequestId}");
        }
    }
}