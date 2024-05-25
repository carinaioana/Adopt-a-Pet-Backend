using AdoptPets.Application.Features.Animals.Commands.UpdateAnimal;
using AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement;
using AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement;
using AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement;
using AdoptPets.Application.Features.Announcements.Queries;
using AdoptPets.Application.Features.Announcements.Queries.GetAll;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{
    public class AnnouncController : ApiControllerBase
    {
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteAnnouncementCommand() { AnnouncementId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateAnnouncementCommandResponse>> Create([FromBody] CreateAnnouncementCommand createAnnouncementCommand)
        {
            var response = await Mediator.Send(createAnnouncementCommand);
            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateAnnouncementCommand command)
        {
            if (id != command.AnnouncementId)
            {
                return BadRequest("The name in the URL does not match the name in the command.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                if (result.Message == "Announcement not found")
                {
                    return NotFound(result.Message);
                }

                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        [HttpGet]
        public async Task<ActionResult<List<AnnouncementDto>>> GetAll()
        {
            var dtos = await Mediator.Send(new GetAllAnouncementsQuery());
            return Ok(dtos);
        }
        [HttpGet("my-announcements")]
        public async Task<ActionResult<GetAnnouncementsByUserQueryResponse>> GetMyAnnouncements()
        {
            var dtos = await Mediator.Send(new GetAnnouncementsByUserQuery());

            if (!dtos.Success)
            {
                return NotFound(dtos);
            }

            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetAnnouncDetailQuery()
            {
                AnnouncementId = id
            });
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result.Announcement);
        }
    }
}
