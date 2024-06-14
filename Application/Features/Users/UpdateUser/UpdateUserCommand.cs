namespace AdoptPets.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommand
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

