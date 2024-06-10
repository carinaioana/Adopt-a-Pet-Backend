using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetDewormingByAnimal
{
    public class GetByAnimalDewormingQueryResponse : BaseResponse
    {
        public GetByAnimalDewormingQueryResponse() : base() { }
        public CreateDewormingDto DewormingDto { get; set; } = default!;
    }
}
