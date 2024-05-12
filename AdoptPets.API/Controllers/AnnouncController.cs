using AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement;
using AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement;
using AdoptPets.Application.Features.Announcements.Queries;
using AdoptPets.Application.Features.Announcements.Queries.GetAll;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncements;
using AdoptPets.Application.Features.Announcements.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult<List<AnnouncementDto>>> GetAll()
        {
            var dtos = await Mediator.Send(new GetAnnouncementsQuery());
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
