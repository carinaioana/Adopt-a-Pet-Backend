using AdoptPets.Application.Contracts.Interfaces;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace AdoptPets.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            {
                return _httpContextAccessor.HttpContext.User;
            }

            return null!;
        }

        public string GetCurrentUserId()
        {
            return UserId;
        }
    }
}
