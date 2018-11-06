using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EndpointRouter.Contracts
{
    /// <summary>
    /// Interface for an endpoint result.
    /// </summary>
    public interface IEndpointResult
    {
        /// <summary>
        /// Executes the endpoint result.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task ExecuteAsync(HttpContext httpContext);
    }
}