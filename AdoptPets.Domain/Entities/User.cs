using BackupMonitoring.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class User : AuditableEntity
    {
        public User(string name, string email, string password, string username)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Username = username;
        }
        [Key]
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private  set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string? Location { get; private set; }
        public string? Description {  get; private set; }
        public List<Animal>? Animals { get; private set; }
        public static Result<User> Create(string name, string email, string password, string username)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<User>.Failure("Name is required");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<User>.Failure("Email is required");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result<User>.Failure("Password is required");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                return Result<User>.Failure("Username is required");
            }
            return Result<User>.Success(new User(name, email, password, username));
        }
        public void AttachLocation(string location)
        {
            if (!string.IsNullOrWhiteSpace(location))
            {
                Location = location;
            }
        }
        public void AttachDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Description = description;
            }
        }

        public void AddAnimal(Animal animal)
        {
            if (Animals == null)
            {
                Animals = new List<Animal>();
            }
            Animals.Add(animal);
        }

    }
}
