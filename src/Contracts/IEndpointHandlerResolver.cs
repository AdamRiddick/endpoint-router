using Microsoft.AspNetCore.Http;

namespace EndpointRouter.Contracts
{
    /// <summary>
    /// Interface for an <see cref="IEndpointHandler"/> resolver.
    /// </summary>
    public interface IEndpointHandlerResolver
    {
        /// <summary>
        /// Gets the <see cref="IEndpointHandler"/> for the request, if one has been registered.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        /// <returns>The <see cref="IEndpointHandler"/> for the request.</returns>
        IEndpointHandler Resolve(HttpContext httpContext);
    }
}