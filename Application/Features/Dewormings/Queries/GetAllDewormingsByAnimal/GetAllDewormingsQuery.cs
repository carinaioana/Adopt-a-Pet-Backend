using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetAllDewormingsByAnimal
{
    public class GetAllDewormingsQuery : IRequest<GetAllDewormingsResponse>
    {
        public Guid AnimalId { get; set; }
    }
}
