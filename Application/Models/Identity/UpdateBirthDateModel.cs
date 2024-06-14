namespace AdoptPets.Application.Models.Identity
{
    public class UpdateBirthDateModel
    {
        public string UserId { get; set; }
        public DateTime NewBirthDate { get; set; }
    }
}
