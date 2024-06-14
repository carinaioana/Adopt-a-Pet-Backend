using AdoptPets.Domain.Entities;
using MediatR;

namespace AdoptPets.Application.Features.FindSimilar
{
    public class FindSimilarAnnouncementsQuery : IRequest<FindSimilarAnnouncementsResponse>
    {
        public Announcement LostAnnouncement { get; set; }

        public FindSimilarAnnouncementsQuery(Announcement lostAnnouncement)
        {
            LostAnnouncement = lostAnnouncement;
        }
    }
}
