using AdoptPets.Application.Persistence;
using MediatR;
using System.Net.Http.Headers;

namespace AdoptPets.Application.Features.Announcements.Commands.DeleteAnnouncement
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, DeleteAnnouncementCommandResponse>
    {
        private readonly IAnnouncementRepository repository;

        public DeleteAnnouncementCommandHandler(IAnnouncementRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteAnnouncementCommandResponse> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.AnnouncementId);
            if (!result.IsSuccess)
            {
                return new DeleteAnnouncementCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteAnnouncementCommandResponse
            {
                Success = true
            };

        }
    }
}
