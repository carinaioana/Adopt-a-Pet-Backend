namespace AdoptPets.Domain.OptionalEntities
{
    public class MedicalHistory
    {
        public Guid MedicalHistoryId { get; set; }
        public Guid AnimalId { get; set; }
        public string? MedicalHistoryName { get; set; }
        public DateTime? MedicalHistoryDate { get; set; }
        public string? MedicalHistoryDescription { get; set; }

    }
}
