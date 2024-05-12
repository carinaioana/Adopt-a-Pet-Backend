namespace AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination
{
    public class CreateVaccinationDto
    {
        public Guid VaccinationId { get;  set; }

        public DateTime Date { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }

    }
}
