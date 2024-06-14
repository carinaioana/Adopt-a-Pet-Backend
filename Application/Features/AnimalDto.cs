using AdoptPets.Application.Features.Dewormings;
using AdoptPets.Application.Features.Observations;
using AdoptPets.Application.Features.Vaccinations;

namespace AdoptPets.Application.Features
{
    public class AnimalDto
    {
        public Guid AnimalId { get; set; }
        public string AnimalType { get; set; } = default!;
        public string AnimalName { get; set; } = default!;
        public string? AnimalDescription { get; set; }
        public List<string>? PersonalityTraits { get; set; }
        public int AnimalAge { get; set; }
        public string? AnimalBreed { get; set; }
        public string? AnimalSex { get; set; }
        public string? ImageUrl { get; set; }
        public MedicalHistoryDto MedicalHistory { get; set; } = default!;
        public ICollection<VaccinationDto> Vaccinations { get; set; } 
        public ICollection<ObservationDto>? Observations { get; set; }
        public ICollection<DewormingDto>? Dewormings { get; set; }
    }

}

