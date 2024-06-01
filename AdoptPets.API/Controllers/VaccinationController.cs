
using AdoptPets.Application.Features.Observations.Queries.GetObservationsByAnimal;
using AdoptPets.Application.Features.Observations;
using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;
using AdoptPets.Application.Features.Vaccinations.Queries.GetAllVaccinations;
using AdoptPets.Application.Features.Vaccinations.Queries.GetByIdVaccination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdoptPets.Application.Features.Vaccinations;
using AdoptPets.Application.Features.Vaccinations.Queries.GetVaccinationsByAnimal;

namespace AdoptPets.API.Controllers
{
    public class VaccinationController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateVaccinationCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllVaccinationsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdVaccinationQuery(id));
            return Ok(result);
        }
        [HttpGet("animal/{animalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VaccinationDto>> GetByAnimalId(Guid animalId)
        {
            var result = await Mediator.Send(new GetVaccinationsByAnimalQuery(animalId));
            return Ok(result);

        }
    }
}
