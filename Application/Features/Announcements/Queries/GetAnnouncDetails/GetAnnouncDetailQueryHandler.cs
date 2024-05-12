using MediatR;
using AdoptPets.Application.Persistence;

namespace AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails
{
    public class GetAnnouncDetailQueryHandler : IRequestHandler<GetAnnouncDetailQuery, GetAnnouncDetailQueryResponse>
    {
        private readonly IAnnouncementRepository repository;

        public GetAnnouncDetailQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAnnouncDetailQueryResponse> Handle(GetAnnouncDetailQuery request, CancellationToken cancellationToken)
        {
            var announc = await repository.FindByIdAsync(request.AnnouncementId);
            if (!announc.IsSuccess)
            {
                return new GetAnnouncDetailQueryResponse
                {
                    Success = false,
                    ValidationsErrors = [ announc.Error ]
                };
            }
            return new GetAnnouncDetailQueryResponse
            {
                Success = true,
                Announcement = new AnnouncementDto
                {
                    AnnouncementId = announc.Value.AnnouncementId,
                    AnnouncementTitle = announc.Value.AnnouncementTitle,
                    AnnouncementDescription = announc.Value.AnnouncementDescription,
                    AnnouncementDate = announc.Value.AnnouncementDate,
                    ImageUrl = announc.Value.ImageUrl,
                   

                }
            };

        }
    }
}
