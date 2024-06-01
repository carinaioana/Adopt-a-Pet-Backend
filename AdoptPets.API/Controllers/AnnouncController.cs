using AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement;
using AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement;
using AdoptPets.Application.Features.Announcements.Queries;
using AdoptPets.Application.Features.Announcements.Queries.GetAll;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser;
using Amazon.S3.Model;
using Amazon.S3;
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
        public async Task<ActionResult<CreateAnnouncementCommandResponse>> Create([FromForm] CreateAnnouncementCommand createAnnouncementCommand, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                createAnnouncementCommand.ImageFile = imageFile;
            }
            else
            {
                return BadRequest(new CreateAnnouncementCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Image upload failed" }
                });
            }

            var response = await Mediator.Send(createAnnouncementCommand);
            return Ok(response);
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
