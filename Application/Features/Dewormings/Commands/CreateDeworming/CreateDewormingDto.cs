namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingDto
    {
        public Guid DewormingId { get; set; }
        public DateTime Date { get; set; }
        public string DewormingType { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }


    }
}
