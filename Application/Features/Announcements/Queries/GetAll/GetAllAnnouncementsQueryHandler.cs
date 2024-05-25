using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;
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
            var result = await repository.GetAllAsync();
            var announcements = result.Value.Select(a => new AnnouncementDto
                {
                    AnnouncementId = a.AnnouncementId,
                    AnnouncementTitle = a.AnnouncementTitle,
                    AnnouncementDate = a.AnnouncementDate,
                    AnnouncementDescription = a.AnnouncementDescription,
                    ImageUrl = a.ImageUrl,
                    CreatedBy = a.CreatedBy
            }).ToList();
            return new GetAllAnnouncementsResponse
            {
                Announcements = announcements,
                Success = true
            };
        }
    }
}
