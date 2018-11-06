using EndpointRouter.Contracts;
using EndpointRouter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EndpointRouter
{
    /// <summary>
    /// Extension methods to add Endpoint Router services to the <see cref="IServiceCollection"/>.
    /// </summary>
    public static class EndpointRouterServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Endpoint Router to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEndpointRouter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEndpointHandlerResolver, EndpointHandlerResolver>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds an endpoint handler.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEndpointHandler"/> <see cref="Type"/>.</typeparam>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="path">The <see cref="PathString"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddEndpointHandler<T>(this IServiceCollection serviceCollection, PathString path)
            where T : class, IEndpointHandler
        {
            serviceCollection.AddTransient<T>();
            serviceCollection.AddSingleton(new Endpoint(typeof(T), path));
            return serviceCollection;
        }
    }
}