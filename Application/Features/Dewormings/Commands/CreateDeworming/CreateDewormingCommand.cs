using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming
{
    public class CreateDewormingCommand : IRequest<CreateDewormingCommandResponse>
    {
        public DateTime Date { get; set; }
        public string DewormingType { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }


    }
}
