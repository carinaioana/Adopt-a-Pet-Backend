﻿using MediatR;

namespace AdoptPets.Application.Features.Vaccinations.Queries.GetAllVaccinations
{
    public class GetAllVaccinationsQuery : IRequest<GetAllVaccinationsResponse>
    {
        public Guid AnimalId { get; set; }

    }
}
