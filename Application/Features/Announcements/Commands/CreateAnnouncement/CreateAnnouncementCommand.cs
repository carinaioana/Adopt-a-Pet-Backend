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

        public string? AnimalType { get;  set; }
        public string? AnimalBreed { get;  set; }
        public string? AnimalGender { get;  set; }
        public string? Location { get;  set; }

    }
}
