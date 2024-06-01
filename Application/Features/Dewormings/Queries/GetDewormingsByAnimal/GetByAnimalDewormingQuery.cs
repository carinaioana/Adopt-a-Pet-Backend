﻿using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using MediatR;

namespace AdoptPets.Application.Features.Dewormings.Queries.GetDewormingsByAnimal
{
    public class GetByAnimalDewormingQuery: IRequest<GetByAnimalDewormingQueryResponse>
    {
        public Guid AnimalId { get; set; }
    }

}
