using AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement;
using AdoptPets.Application.Features.Dewormings.Commands.DeleteDeworming;
using AdoptPets.Application.Features.Observations.Commands.CreateObservation;
using AdoptPets.Application.Features.Observations.Commands.DeleteObservation;
using AdoptPets.Application.Features.Observations.Commands.UpdateObservation;
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateObservationCommand command)
        {
            if (id != command.ObservationId)
            {
                return BadRequest("The name in the URL does not match the name in the command.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                if (result.Message == "Observation not found")
                {
                    return NotFound(result.Message);
                }

                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteObservationCommand() { ObservationId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
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
