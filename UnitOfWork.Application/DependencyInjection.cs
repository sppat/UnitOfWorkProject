using Microsoft.Extensions.DependencyInjection;
using UnitOfWork.Application.Commands.Product;

namespace UnitOfWork.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

            return services;
        }
    }
}
