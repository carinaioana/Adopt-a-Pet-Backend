namespace AdoptPets.Application.Features.Vaccinations
{
    public class VaccinationDto
    {
        public Guid VaccinationId { get; set; }
        public DateTime Date { get; set; }
        public string VaccineName { get; set; } = default!;
        public Guid AnimalId { get; set; }
    }
}
