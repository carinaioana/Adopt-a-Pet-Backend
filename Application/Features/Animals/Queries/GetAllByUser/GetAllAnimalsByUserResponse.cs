using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Animals.Queries.GetAllByUser
{
    public class GetAllAnimalsByUserResponse : BaseResponse
    {
        public GetAllAnimalsByUserResponse() : base() { }
        public List<AnimalDto> Animals { get; set; } = default!;
    }
}
