using System;
using EndpointRouter.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EndpointRouter
{
    /// <summary>
    /// Extensions methods to add Endpoint Router to the <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class EndpointRouterApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds Endpoint Router to the pipeline.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseEndpointRouter(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Validate();
            applicationBuilder.UseMiddleware<EndpointRouterMiddleware>();
            return applicationBuilder;
        }

        /// <summary>
        /// Validates the application can add the <see cref="EndpointRouterMiddleware"/>.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
        internal static void Validate(this IApplicationBuilder applicationBuilder)
        {
            var loggerFactory = applicationBuilder.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            if(loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("EndpointRouter.Startup");
            var scopeFactory = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>();

            logger.LogTrace("Validating service registration.");
            using(var scope = scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                ValidateServiceRegistration(serviceProvider, typeof(IEndpointHandlerResolver), logger);
            }
            logger.LogTrace("Service registration validated.");
        }

        /// <summary>
        /// Validates a service has been registered.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        /// <param name="service">The <see cref="Type"/> to validate has been registered.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <returns></returns>
        internal static void ValidateServiceRegistration(IServiceProvider serviceProvider, Type service, ILogger logger)
        {
            var appService = serviceProvider.GetService(service);
            if(appService == null)
            {
                var errorMessage = $"Required service {service.FullName} has not been registered - have you called AddEndpointRouter?";
                logger.LogCritical(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}