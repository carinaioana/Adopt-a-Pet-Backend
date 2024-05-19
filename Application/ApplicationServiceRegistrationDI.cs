using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace AdoptPets.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR
                (
                    cfg =>
                        cfg.RegisterServicesFromAssembly(
                                            Assembly.GetExecutingAssembly())
                 );
        }
    }
}
