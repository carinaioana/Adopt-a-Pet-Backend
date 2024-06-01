using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Features.Dewormings.Queries.GetDewormingsByAnimal;
using AdoptPets.Application.Features.Observations;
using AdoptPets.Application.Features.Observations.Commands.CreateObservation;
using AdoptPets.Application.Features.Observations.Queries.GetAllObservations;
using AdoptPets.Application.Features.Observations.Queries.GetByIdObservation;
using AdoptPets.Application.Features.Observations.Queries.GetObservationsByAnimal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{

    public class ObservationController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateObservationCommand command)
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
            var result = await Mediator.Send(new GetAllObservationsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdObservationQuery(id));
            return Ok(result);
        }
        [HttpGet("animal/{animalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObservationDto>> GetByAnimalId(Guid animalId)
        {
            var result = await Mediator.Send(new GetObservationsByAnimalQuery(animalId));
            return Ok(result);

        }
    }
}
