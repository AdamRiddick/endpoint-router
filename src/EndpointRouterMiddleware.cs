using System;
using System.Threading.Tasks;
using EndpointRouter.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EndpointRouter
{
    /// <summary>
    /// Endpoint Router middleware
    /// </summary>
    public class EndpointRouterMiddleware
    {
        private readonly ILogger _logger;

        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointRouterMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next <see cref="RequestDelegate"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        public EndpointRouterMiddleware(RequestDelegate next, ILogger<EndpointRouterMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="endpointHandlerResolver">The <see cref="IEndpointHandlerResolver"/>.</param>
        /// <returns><see cref="Task"/></returns>
        public async Task Invoke(HttpContext httpContext, IEndpointHandlerResolver endpointHandlerResolver)
        {
            try
            {
                var endpointHandler = endpointHandlerResolver.Resolve(httpContext);
                if(endpointHandler != null)
                {
                    _logger.LogInformation("Invoking endpoint handler: {0} for path {1}", endpointHandler.GetType().FullName, httpContext.Request.Path.ToString());

                    var result = await endpointHandler.ProcessAsync(httpContext);
                    if(result != null)
                    {
                        _logger.LogTrace("Invoking endpoint result: {0}", result.GetType().FullName);
                        await result.ExecuteAsync(httpContext);
                    }

                    return;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "An unhandled exception occurred while handling a routed endpoint: {0}", ex.Message);
                throw;
            }

            await _next(httpContext);
        }
    }
}