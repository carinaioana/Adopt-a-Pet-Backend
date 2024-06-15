using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByTitle
{
    public class GetByTitleAnnouncQueryHandler
    {
        private readonly IAnnouncementRepository repository;
        public GetByTitleAnnouncQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }
    }
}
