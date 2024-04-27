using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandResponse : BaseResponse
    {
        public CreateAnimalCommandResponse() : base() { }
        public CreateAnimalDto Animal {  get; set; }
    }
}
