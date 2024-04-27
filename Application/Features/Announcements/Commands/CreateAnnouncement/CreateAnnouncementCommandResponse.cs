using AdoptPets.Application.Responses;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommandResponse : BaseResponse
    {
        public CreateAnnouncementCommandResponse() : base() { }
        public CreateAnnouncementDto Announcement { get; set; }
    }
}
