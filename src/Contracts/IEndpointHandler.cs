using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EndpointRouter.Contracts
{
    /// <summary>
    /// Interface for an endpoint handler.
    /// </summary>
    public interface IEndpointHandler
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        /// <returns>An <see cref="Task{IEndpointResult}"/>.</returns>
        Task<IEndpointResult> ProcessAsync(HttpContext httpContext);
    }
}