namespace AdoptPets.Application.Features.Observations
{
    public class ObservationDto
    {
        public Guid ObservationId { get; set; }
        public DateTime Date { get; set; }
        public string ObservationDescription { get; set; } = default!;
        public Guid AnimalId { get; set; }
    }
}
