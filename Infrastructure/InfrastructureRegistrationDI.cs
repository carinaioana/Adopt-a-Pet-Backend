using AdoptPets.Application.Contracts;
using AdoptPets.Application.Persistence;
using AdoptPets.Infrastructure.Repositories;
using AdoptPets.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdoptPets.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<AdoptPetsContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("AdoptPetsConnection"), 
                    builder => 
                    builder.MigrationsAssembly(
                        typeof(AdoptPetsContext).
                        Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IVaccinationRepository, VaccinationRepository>();
            services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
            services.AddScoped<IDewormingRepository, DewormingRepository>();
            services.AddScoped<IObservationRepository, ObservationRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
