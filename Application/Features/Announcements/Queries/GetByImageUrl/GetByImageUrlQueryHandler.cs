using MediatR;
using AdoptPets.Application.Persistence;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncDetails;

namespace AdoptPets.Application.Features.Announcements.Queries.GetByImageUrl
{
    public class GetByImageUrlQueryHandler : IRequestHandler<GetByImageUrlQuery, GetByImageUrlQueryResponse>
    {
        private readonly IAnnouncementRepository _repository;

        public GetByImageUrlQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetByImageUrlQueryResponse> Handle(GetByImageUrlQuery request, CancellationToken cancellationToken)
        {
            var announc = await _repository.FindByImageAsync(request.ImageUrl);
            if (!announc.IsSuccess)
            {
                return new GetByImageUrlQueryResponse
                {
                    Success = false,
                    ValidationsErrors = [announc.Error]
                };
            }
            return new GetByImageUrlQueryResponse
            {
                Success = true,
                Announcement = new AnnouncementDto
                {
                    AnnouncementId = announc.Value.AnnouncementId,
                    AnnouncementTitle = announc.Value.AnnouncementTitle,
                    AnnouncementDescription = announc.Value.AnnouncementDescription,
                    AnnouncementDate = announc.Value.AnnouncementDate,
                    ImageUrl = announc.Value.ImageUrl,
                    AnimalBreed = announc.Value.AnimalBreed,
                    AnimalGender = announc.Value.AnimalGender,
                    AnimalType = announc.Value.AnimalType,
                    Location = announc.Value.Location,


                }
            };
        }
    }

}
