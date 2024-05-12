namespace AdoptPets.Application.Features.Observations.Commands.CreateObservation
{
    public class CreateObservationDto
    {
        public Guid ObservationId { get; set; }
        public DateTime Date { get; set; }
        public string ObservationDescription { get; set; } = default!;
        public Guid AnimalId { get; set; }

    }
}
