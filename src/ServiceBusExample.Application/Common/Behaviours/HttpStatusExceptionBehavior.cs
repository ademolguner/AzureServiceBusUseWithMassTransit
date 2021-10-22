using HepsiExpress.StudyCase.Api.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Behaviours
{
    public class HttpStatusExceptionBehavior
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpStatusExceptionBehavior> _logger;

        public HttpStatusExceptionBehavior(RequestDelegate next, ILogger<HttpStatusExceptionBehavior> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiceBusExample Request went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCode(ex);
            await JsonSerializer.SerializeAsync(httpContext.Response.Body, ResponseException.Create(ex), null, httpContext.RequestAborted);
        }

        private static int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                ArgumentOutOfRangeException or
                ArgumentException or
                InvalidOperationException or
                NullReferenceException
                  => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}