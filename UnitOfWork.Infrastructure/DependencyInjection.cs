using Microsoft.Extensions.DependencyInjection;
using UnitOfWork.Application.Interfaces.Repositories;

namespace UnitOfWork.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, Repositories.UnitOfWork>();

            return services;
        }
    }
}
