using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAll
{
    public class GetAllAnnouncementsQueryHandler : IRequestHandler<GetAllAnouncementsQuery, GetAllAnnouncementsResponse>
    {
        private readonly IAnnouncementRepository repository;

        public GetAllAnnouncementsQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllAnnouncementsResponse> Handle(GetAllAnouncementsQuery request, CancellationToken cancellationToken)
        {
            GetAllAnnouncementsResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Announcements = result.Value.Select(announcement => new AnnouncementDto
                {
                    AnnouncementId = announcement.AnnouncementId,
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    AnnouncementDate = announcement.AnnouncementDate,
                    UserId = announcement.UserId
                }).ToList();
            }
            return response;
        }
    }
}
