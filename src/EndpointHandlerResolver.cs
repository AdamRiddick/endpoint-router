using System;
using System.Collections.Generic;
using EndpointRouter.Contracts;
using EndpointRouter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EndpointRouter
{
    internal class EndpointHandlerResolver : IEndpointHandlerResolver
    {
        private readonly IEnumerable<Endpoint> _endpoints;

        private readonly ILogger _logger;

        public EndpointHandlerResolver(IEnumerable<Endpoint> endpoints, ILogger<EndpointHandlerResolver> logger)
        {
            _endpoints = endpoints;
            _logger = logger;
        }

        public IEndpointHandler Resolve(HttpContext httpContext)
        {
            _logger.LogTrace("Resolving Endpoint Handler.");

            if(httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            foreach(var endpoint in _endpoints)
            {
                if(httpContext.Request.Path.Equals(endpoint.Path, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogTrace("Path {0} matched to endpoint handler {1}.", httpContext.Request.Path, endpoint.EndpointHandlerType.FullName);
                    return GetEndpointHandler(endpoint, httpContext);
                }
            }

            _logger.LogTrace("No endpoint handler found for path: {0}.", httpContext.Request.Path);
            return null;
        }

        /// <summary>
        /// Gets an <see cref="IEndpointHandler"/> instance.
        /// </summary>
        /// <param name="endpoint">The <see cref="Endpoint"/>.</param>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        /// <returns><see cref="IEndpointHandler"/>.</returns>
        private IEndpointHandler GetEndpointHandler(Endpoint endpoint, HttpContext httpContext)
        {
            if(httpContext.RequestServices.GetService(endpoint.EndpointHandlerType) is IEndpointHandler handler)
            {
                _logger.LogTrace("Created endpoint handler: {0}.", endpoint.EndpointHandlerType.FullName);
                return handler;
            }

            _logger.LogError("Failed to create endpoint handler: {0}.", endpoint.EndpointHandlerType.FullName);
            return null;
        }
    }
}