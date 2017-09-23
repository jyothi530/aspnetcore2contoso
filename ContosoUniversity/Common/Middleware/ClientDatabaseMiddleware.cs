using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Common.Middleware
{
    public class ClientDatabaseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientDatabaseMiddleware> _logger;

        public ClientDatabaseMiddleware(RequestDelegate next, ILogger<ClientDatabaseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            var clientHeader = context.Request.Headers["X-Requested-By-Client"];

            if (clientHeader.Count == 0)
            {
                _logger.LogInformation("X-Requested-By-Client header is not specified for path:{0}", context.Request.Path);
                return _next(context);
            }

            Console.WriteLine("X-Requested-With-Client: {0}", clientHeader.Single());

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
