using Microsoft.AspNetCore.Builder;

namespace ContosoUniversity.Common.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseClientDatabase(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ClientDatabaseMiddleware>();
        }
    }
}
