using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncements
{
    public class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, GetAnnouncementsQueryResponse>
    {
        private readonly IAnnouncementRepository repository;

        public GetAnnouncementsQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAnnouncementsQueryResponse> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var result =  await repository.GetAllAsync();
            var announcements = result.Value.Select(e => new AnnouncementDto
            {
                AnnouncementId = e.AnnouncementId,
                AnnouncementTitle = e.AnnouncementTitle,
                AnnouncementDescription = e.AnnouncementDescription,
                AnnouncementDate = e.AnnouncementDate,
               
            }).ToList();
            return new GetAnnouncementsQueryResponse
            { 
                Announcements = announcements, 
                Success = true
            };
        }
    }
}
