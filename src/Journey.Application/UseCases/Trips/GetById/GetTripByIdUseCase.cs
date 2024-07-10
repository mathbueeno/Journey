using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetTripById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id)
        {
            var dbContext = new JorneyDbContext();

            var trips = dbContext
                .Trips
                .Include(trips => trips.Activities)
                .FirstOrDefault(trip => trip.Id == id); // LAMBDA

            // is é a nova forma de utilizar ==
            if (trips is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Id = trips.Id,
                StartDate = trips.StartDate,
                EndDate = trips.EndDate,
                Name = trips.Name,
                Activities = trips.Activities.Select(activity => new ResponseActivityJson
                {
                    Id = activity.Id,
                    Name = activity.Name,
                    Date = activity.Date,
                    Status = (Communication.Enums.ActivityStatus)activity.Status
                }).ToList()
            };
        }
    }
}
