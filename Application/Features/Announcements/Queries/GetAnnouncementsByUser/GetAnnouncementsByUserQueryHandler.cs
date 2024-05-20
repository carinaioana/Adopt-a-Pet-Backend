using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser
{
    public class GetAnnouncementsByUserQueryHandler : IRequestHandler<GetAnnouncementsByUserQuery, GetAnnouncementsByUserQueryResponse>
    {
        private readonly IAnnouncementRepository repository;
        private readonly ICurrentUserService currentUserService;

        public GetAnnouncementsByUserQueryHandler(IAnnouncementRepository repository, ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public async Task<GetAnnouncementsByUserQueryResponse> Handle(GetAnnouncementsByUserQuery request, CancellationToken cancellationToken)
        {
            string userId = currentUserService.GetCurrentUserId();

            // Query the repository for entities created by the current user
            var announcements = await repository.GetAnnouncementsByUserAsync(userId);

            var announcementDtos = announcements.Select(e => new AnnouncementDto
            {
                AnnouncementId = e.AnnouncementId,
                AnnouncementTitle = e.AnnouncementTitle,
                AnnouncementDescription = e.AnnouncementDescription,
                AnnouncementDate = e.AnnouncementDate,
            }).ToList();

            return new GetAnnouncementsByUserQueryResponse
            {
                Announcements = announcementDtos,
                Success = true
            };
        }
    }
}
