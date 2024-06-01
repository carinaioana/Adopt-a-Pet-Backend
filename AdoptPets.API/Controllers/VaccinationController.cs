
using AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement;
using AdoptPets.Application.Features.Dewormings.Commands.DeleteDeworming;
using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;
using AdoptPets.Application.Features.Vaccinations.Commands.DeleteVaccination;
using AdoptPets.Application.Features.Vaccinations.Commands.UpdateVaccination;
using AdoptPets.Application.Features.Vaccinations.Queries.GetAllVaccinations;
using AdoptPets.Application.Features.Vaccinations.Queries.GetByIdVaccination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateVaccinationCommand command)
        {
            if (id != command.VaccinationId)
            {
                return BadRequest("The name in the URL does not match the name in the command.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                if (result.Message == "Vaccination not found")
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
            var deleteEventCommand = new DeleteVaccinationCommand() { VaccinationId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
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
    }
}
