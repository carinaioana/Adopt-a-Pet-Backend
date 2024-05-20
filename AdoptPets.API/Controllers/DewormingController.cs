using AdoptPets.Application.Features.Dewormings.Commands.CreateDeworming;
using AdoptPets.Application.Features.Dewormings.Queries.GetAllDewormings;
using AdoptPets.Application.Features.Dewormings.Queries.GetByIdDeworming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement;
using AdoptPets.Application.Features.Dewormings.Commands.DeleteDeworming;

namespace AdoptPets.API.Controllers
{
    public class DewormingController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateDewormingCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteDewormingCommand() { DewormingId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllDewormingsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateDewormingDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdDewormingQuery(id));
            return Ok(result);
        }
    }
}
