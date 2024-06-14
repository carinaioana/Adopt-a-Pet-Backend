using AdoptPets.Application.Persistence;
using MediatR;


namespace AdoptPets.Application.Features.FindSimilar
{
    public class FindSimilarAnnouncementsQueryHandler : IRequestHandler<FindSimilarAnnouncementsQuery, FindSimilarAnnouncementsResponse>
    {
        private readonly IAnnouncementRepository _repository;
     

        public FindSimilarAnnouncementsQueryHandler(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<FindSimilarAnnouncementsResponse> Handle(FindSimilarAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
