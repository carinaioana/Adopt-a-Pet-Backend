using AdoptPets.Application.Models;

namespace AdoptPets.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);

    }
}
