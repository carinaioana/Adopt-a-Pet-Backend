using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandResponse : BaseResponse
    {
        public UpdateAnnouncementCommandResponse() : base()
        {

        }
        public string Message { get; set; }
    }

}
