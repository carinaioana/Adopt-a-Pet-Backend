using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Animal : AuditableEntity
    {
        public Animal(string animalName, string animalType)
        {
            AnimalId = Guid.NewGuid();
            AnimalName = animalName;
            AnimalType = animalType;
        }

        [Key]
        public Guid AnimalId { get; private set; }
        public string AnimalName { get; private set; } = string.Empty;
        public string? AnimalDescription { get; private set; }
        public string AnimalType { get; private set; } = string.Empty;
        public int AnimalAge { get; private set; }
        public string? AnimalBreed { get; private set; }
        public string? AnimalSex { get; private set; }
        public List<string>? PersonalityTraits { get; private set; }
        public string? ImageUrl { get; private set; }

        public MedicalHistory MedicalHistory { get; set; }
        public Guid MedicalHistoryId { get; set; }
        public ICollection<Vaccination> Vaccinations { get; set; }
        public ICollection<Observation> Observations { get; set; }
        public ICollection<Deworming> Dewormings { get; set; }

        public static Result<Animal> Create(string animalName, string animalType)
        {
            if (string.IsNullOrWhiteSpace(animalType))
            {
                return Result<Animal>.Failure("AnimalType is required");
            }
            if (string.IsNullOrWhiteSpace(animalName))
            {
                return Result<Animal>.Failure("AnimalName is required");
            }
            return Result<Animal>.Success(new Animal(animalName, animalType));
        }
        public void AttachImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }

        public void AttachPersonalityTraits(List<string> traits)
        {
            if (traits != null && traits.Any())
            {
                PersonalityTraits = traits;
            }
        }

        public void AttachDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
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
        public void AttachSex(string sex)
        {
            if (!string.IsNullOrEmpty(sex))
            {
                AnimalSex = sex;
            }
        }

        public void SetAge(int age)
        {
            if (age > 0)
            {
                AnimalAge = age;
            }
        }
        public void AttachMedicalHistoryId(Guid medicalHistoryId)
        {
            MedicalHistoryId = medicalHistoryId;
        }
        public void Update(string animalName, string animalType, int animalAge, string? animalBreed, string? animalSex, string? imageUrl, string? animalDescription, List<string>? personalityTraits)
        {
            AnimalAge = animalAge;
            AnimalName = animalName;
            AnimalType = animalType;
            AnimalBreed = animalBreed;
            AnimalSex = animalSex;
            ImageUrl = imageUrl;
            AnimalDescription = animalDescription;
            PersonalityTraits = personalityTraits;

        }

        public void UpdateType(string animalType)
        {
            if(!string.IsNullOrEmpty(animalType))
            {
                AnimalType = animalType;
            }
        }

        public void UpdateName(string animalName)
        {
            if (!string.IsNullOrEmpty(animalName))
            {
                AnimalName = animalName;
            }
        }

        public void UpdateAge(int animalAge)
        {
            if(animalAge > 0)
            {
                AnimalAge = animalAge;
            }
        }

        public void UpdateBreed(string animalBreed)
        {
           if (!string.IsNullOrEmpty(animalBreed))
            {
                AnimalBreed = animalBreed;
            }
        }

        public void UpdateSex(string animalSex)
        {
            if (!string.IsNullOrEmpty(animalSex))
            {
                AnimalSex = animalSex;
            }
        }

        public void UpdateImageUrl(string imageUrl)
        {
            if(!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }

        public void UpdateDescription(string animalDescription)
        {
            if(!string.IsNullOrEmpty(animalDescription))
            {
                AnimalDescription = animalDescription;
            }
        }

        public void UpdatePersonalityTraits(List<string> personalityTraits)
        {
            if(personalityTraits != null && personalityTraits.Any() )
            {
                PersonalityTraits = personalityTraits;
            }
        }
    }
}
