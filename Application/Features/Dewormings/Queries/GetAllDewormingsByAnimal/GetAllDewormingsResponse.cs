using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetAllDewormingsByAnimal
{
    public class GetAllDewormingsResponse
    {
        public List<DewormingDto> Dewormings { get; set; } = default!;

    }
}
