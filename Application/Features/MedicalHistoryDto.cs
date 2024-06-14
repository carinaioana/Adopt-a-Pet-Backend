namespace AdoptPets.Application.Features
{
    public class MedicalHistoryDto
    {
        public Guid MedicalHistoryId { get; set; }
        public string UserId { get; set; } = default!;
        public Guid AnimalId { get; set; }
        public AnimalDto Animal { get; set; }
/*        public DewormingDto Deworming { get; set; }
        public VaccinationDto Vaccination { get; set; }
        public ObservationDto Observation { get; set; }*/

    }
}
