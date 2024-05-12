using AdoptPets.Application.Features;
using AdoptPets.Application.Features.MedicalHistories.Commands.CreateMedicalHistory;
using AdoptPets.Application.Features.MedicalHistories.Commands.DeleteMedicalHistory;
using AdoptPets.Application.Features.MedicalHistories.Queries.GetAllMedicalHistories;
using AdoptPets.Application.Features.MedicalHistories.Queries.GetByIdMedicalHistory.GetByIdMedicalHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{

    public class MedicalHistoryController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateMedicalHistoryCommand command)
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
            var deleteEventCommand = new DeleteMedicalHistoryCommand() { MedicalHistoryId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalHistoryDto>>> GetAll()
        {
            var dtos = await Mediator.Send(new GetAllMedicalHistoriesQuery());
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdMedicalHistoryQuery(id));
            return Ok(result);
        }
    }
}
