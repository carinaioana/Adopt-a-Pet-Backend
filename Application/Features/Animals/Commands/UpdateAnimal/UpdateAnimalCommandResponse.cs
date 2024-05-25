using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandResponse : BaseResponse
    {
       public UpdateAnimalCommandResponse() : base()
        {
        }
        public string Message { get; set; }

    }
}
