using AdoptPets.Application.Persistence;
using MediatR;

namespace AdoptPets.Application.Features.Announcements.Queries.GetById
{
    public class GetByIdAnnouncQueryHandler :  IRequestHandler<GetByIdAnnouncQuery, AnnouncementDto>
    {
        private readonly IAnnouncementRepository repository;
        public GetByIdAnnouncQueryHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AnnouncementDto> Handle(GetByIdAnnouncQuery request, CancellationToken cancellationToken)
        {
            var announc = await repository.FindByIdAsync(request.id);
            if (announc.IsSuccess)
            {
                return new AnnouncementDto
                {
                    AnnouncementId = announc.Value.AnnouncementId,
                    AnnouncementTitle = announc.Value.AnnouncementTitle,
                    UserId = announc.Value.UserId,
                    AnnouncementDate = announc.Value.AnnouncementDate
                };
            }
            return new AnnouncementDto();
        }
    }
}
