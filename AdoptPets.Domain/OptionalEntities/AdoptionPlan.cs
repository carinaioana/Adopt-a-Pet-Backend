namespace AdoptPets.Domain.OptionalEntities
{
    public class AdoptionPlan
    {
        public Guid AdoptionPlanId { get; set; }
        public Guid AnimalId { get; set; }
        public string? PlanName { get; set; }
        public string? PlanType { get; set; }
        public string? PlanDescription { get; set; }


    }
}
