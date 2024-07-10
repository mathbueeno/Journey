using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripUseCase
    {
        
            public void Execute(Guid id)
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

                dbContext.Trips.Remove(trips);
                dbContext.SaveChanges();

            }
        
    }
}
