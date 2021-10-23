using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Behaviours
{
    public class LoggingBehaviour
    {
        private readonly RequestDelegate _next;

        public LoggingBehaviour(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, ILogger<LoggingBehaviour> logger)
        {
            if (!context.Request.Path.Value.StartsWith("/health"))
            {
                logger.LogInformation("ServiceBusExample Request: {TraceIdentifier} | {Path} | {@User}",
                context.TraceIdentifier, context.Request.Path,
                "-1");
            }
            return _next(context);
        }
    }
}