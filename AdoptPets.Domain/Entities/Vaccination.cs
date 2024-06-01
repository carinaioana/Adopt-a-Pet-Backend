using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Vaccination : AuditableEntity
    {
        public Vaccination(Guid animalId, DateTime date, string vaccineName)
        {
            VaccinationId = Guid.NewGuid();
            AnimalId = animalId;
            Date = date;
            VaccineName = vaccineName;
        }

        [Key]
        public Guid VaccinationId { get; private set; }
        public DateTime Date { get; private set; }
        public string VaccineName { get; private set; } = string.Empty;
        public Guid AnimalId { get; private set; }
        public Animal Animal { get; private set; }

        public static Result<Vaccination> Create(Guid animalId, DateTime date, string vaccineName)
        {
            if (date == default)
            {
                return Result<Vaccination>.Failure("Date is required.");
            }
            if (string.IsNullOrWhiteSpace(vaccineName))
            {
                return Result<Vaccination>.Failure("VaccineName is required.");
            }
            if (animalId == Guid.Empty)
            {
                return Result<Vaccination>.Failure("AnimalId is required.");
            }
            return Result<Vaccination>.Success(new Vaccination(animalId, date, vaccineName));
        }
        public void Update(DateTime date, string vaccineName)
        {
            if (date == default)
            {
                throw new ArgumentNullException(nameof(date), "Date is required.");
            }
            if (string.IsNullOrWhiteSpace(vaccineName))
            {
                throw new ArgumentNullException(nameof(vaccineName), "VaccineName is required.");
            }
            Date = date;
            VaccineName = vaccineName;
        }
        public void UpdateVaccineName(string vaccineName)
        {
            if (string.IsNullOrWhiteSpace(vaccineName))
            {
                throw new ArgumentNullException(nameof(vaccineName), "VaccineName is required.");
            }
            VaccineName = vaccineName;
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