using System;
using Microsoft.AspNetCore.Http;

namespace EndpointRouter.Model
{
    /// <summary>
    /// Endpoint class.
    /// </summary>
    internal class Endpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Endpoint"/> class.
        /// </summary>
        /// <param name="endpointHandlerType">The <see cref="IEndpointHandler"/> <see cref="Type"/>.</param>
        /// <param name="path">The <see cref="PathString"/> the endpoint processes.</param>
        internal Endpoint(Type endpointHandlerType, string path)
        {
            EndpointHandlerType = endpointHandlerType;
            Path = path;
        }

        /// <summary>
        /// The <see cref="IEndpointHandler"/> <see cref="Type"/>.
        /// </summary>
        public Type EndpointHandlerType { get; set; }

        /// <summary>
        /// The <see cref="PathString"/> the endpoint processes.
        /// </summary>
        public PathString Path { get; set; }
    }
}