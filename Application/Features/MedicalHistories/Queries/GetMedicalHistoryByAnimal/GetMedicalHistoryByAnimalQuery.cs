using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System
    .Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Features.MedicalHistories.Queries.GetMedicalHistoryByAnimal
{
    public record GetMedicalHistoryByAnimalQuery(Guid id) : IRequest<MedicalHistoryDto>;
}
