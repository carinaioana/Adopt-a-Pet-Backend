﻿using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Application.Persistence
{
    public interface IObservationRepository : IAsyncRepository<Observation>
    {
         Task<bool> IsObservationDescriptionAndDateUnique(string observationDescription, DateTime date);
        Task<Result<Observation>> FindByAnimalId(Guid animalId);
        Task<Result<IReadOnlyList<Observation>>> GetAllByAnimalIdAsync(Guid animalId);
    }

}
