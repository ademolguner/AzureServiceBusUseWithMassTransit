using Microsoft.AspNetCore.Builder;
using ServiceBusExample.Application.Common.Behaviours;

namespace ServiceBusExample.Api.Middlewares
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder ConfigureCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingBehaviour>();
            app.UseMiddleware<UnhandledExceptionBehaviour>();
            app.UseMiddleware<HttpStatusExceptionBehavior>();
            return app;
        }
    }
}