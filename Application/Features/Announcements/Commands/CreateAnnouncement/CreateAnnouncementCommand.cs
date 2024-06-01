using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommand :  IRequest<CreateAnnouncementCommandResponse>
    {

        public string AnnouncementTitle { get; set; } = string.Empty;
        public DateTime AnnouncementDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? AnnouncementDescription { get; set; }
        public IFormFile? ImageFile { get; set; } 

    }
}
