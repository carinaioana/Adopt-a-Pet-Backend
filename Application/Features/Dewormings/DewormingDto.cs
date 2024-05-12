namespace AdoptPets.Application.Features.Dewormings
{
    public class DewormingDto
    {
        public Guid DewormingId { get; set; }
        public DateTime Date { get; set; }
        public string DewormingType { get; set; } = default!;
        public Guid AnimalId { get; set; }

    }
}
