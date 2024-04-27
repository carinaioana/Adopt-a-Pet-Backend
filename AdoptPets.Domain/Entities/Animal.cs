using BackupMonitoring.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Animal : AuditableEntity
    {
        public Animal(string animalType, Guid userId, bool isAdopted)
        {
            AnimalId = Guid.NewGuid();
            AnimalType = animalType;
            UserId = userId;
            IsAdopted = isAdopted;
        }
        [Key]
        public Guid AnimalId { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsAdopted { get; private set; }
        public string ? AnimalName { get; private set; }
        public  List<string>? AnimalDescription { get; private set; }
        public string AnimalType { get; private set; } = string.Empty;
        public int AnimalAge {  get; private set; }
        public string ? AnimalBreed { get; private set; }

        /* public int UserId { get; set; } // Foreign Key

         [ForeignKey("UserId")]
         public required User User { get; set; }*/

        public static Result<Animal> Create(string animalType, Guid userId)
        {
             if (string.IsNullOrWhiteSpace(animalType))
            {
                return Result<Animal>.Failure("AnimalType is required");
            }
            if (userId == Guid.Empty)
            {
                return Result<Animal>.Failure("UserId is required");
            }
            return Result<Animal>.Success(new Animal(animalType, userId, false));
        }

        public void AttachName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AnimalName = name;
            }
        }

        public void AttachDescription(List<string> description)
        {
            if (description != null && description.Any())
            {
                AnimalDescription = description;
            }
        }

        public void AttachBreed(string breed)
        {
            if (!string.IsNullOrEmpty(breed))
            {
                AnimalBreed = breed;
            }
        }

        public void SetAge(int age)
        {
            if (age > 0)
            {
                AnimalAge = age;
            }
        }

        public void MarkAsAdopted()
        {
            IsAdopted = true;
        }

    }
}
