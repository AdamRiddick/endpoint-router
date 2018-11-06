using System.Threading.Tasks;
using EndpointRouter.Contracts;
using EndpointRouterDemo.Endpoints.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EndpointRouterDemo.Endpoints
{
    public class DemoEndpointHandler : IEndpointHandler
    {
        private readonly ILogger _logger;

        public DemoEndpointHandler(ILogger<DemoEndpointHandler> logger)
        {
            _logger = logger;
        }

        public Task<IEndpointResult> ProcessAsync(HttpContext httpContext)
        {
            _logger.LogInformation("Entering Demo Endpoint Handler");
            return Task.FromResult(new DemoEndpointResult() as IEndpointResult);
        }
    }
}