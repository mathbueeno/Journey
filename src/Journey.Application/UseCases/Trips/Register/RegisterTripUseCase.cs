using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JorneyDbContext();

            var entity = new Trip()
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            dbContext.Trips.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Name = entity.Name
            };
        }
        

        // Função auxiliar

        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.NAME_EMPTY);
            }

            //UTC hora base de todos os paises
            if (request.StartDate < DateTime.UtcNow.Date)
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
            }

            if (request.EndDate < DateTime.UtcNow.Date)
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
            }
        }
    }
}
