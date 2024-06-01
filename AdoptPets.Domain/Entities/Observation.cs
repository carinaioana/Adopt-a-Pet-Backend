using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Observation : AuditableEntity
    {
        public Observation(Guid animalId, DateTime date, string observationDescription)
        {
            ObservationId = Guid.NewGuid();
            Date = date;
            ObservationDescription = observationDescription;
            AnimalId = animalId;
        }

        [Key]
        public Guid ObservationId { get; private set; }
        public DateTime Date { get; private set; }
        public string ObservationDescription { get; private set; } = string.Empty;
        public Guid AnimalId { get; private set; }
        public Animal Animal { get; private set; }

        public static Result<Observation> Create(Guid animalId, DateTime date, string observationDescription)
        {
            if (date == default)
            {
                return Result<Observation>.Failure("Date is required.");
            }
            if (string.IsNullOrWhiteSpace(observationDescription))
            {
                return Result<Observation>.Failure("Observation is required.");
            }
            if (animalId == Guid.Empty)
            {
                return Result<Observation>.Failure("AnimalId is required.");
            }
            return Result<Observation>.Success(new Observation(animalId, date, observationDescription));
        }
        public void Update(DateTime date, string observationDescription)
        {
            if (date == default)
            {
                throw new ArgumentNullException(nameof(date), "Date is required.");
            }
            if (string.IsNullOrWhiteSpace(observationDescription))
            {
                throw new ArgumentNullException(nameof(observationDescription), "Observation is required.");
            }
            Date = date;
            ObservationDescription = observationDescription;
        }
        public void UpdateObservationDescription(string observationDescription)
        {
            if (string.IsNullOrWhiteSpace(observationDescription))
            {
                throw new ArgumentNullException(nameof(observationDescription), "Observation is required.");
            }
            ObservationDescription = observationDescription;
        }
        public void UpdateDate(DateTime date)
        {
            if (date == default)
            {
                throw new ArgumentNullException(nameof(date), "Date is required.");
            }
            Date = date;
        }

    }
}