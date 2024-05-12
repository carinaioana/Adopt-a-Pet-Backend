using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Entities;

namespace AdoptPets.Infrastructure.Repositories
{
    public class DewormingRepository : BaseRepository<Deworming>, IDewormingRepository
    {
        public DewormingRepository(AdoptPetsContext context) : base(context)
        {
            // Constructor implementation
        }
        public Task<bool> IsDewormingTypeAndDateUnique(string dewormingType, DateTime date)
        {
            var matches = context.Dewormings.Any(a => a.DewormingType.Equals(dewormingType)
            && a.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }
    }
}
