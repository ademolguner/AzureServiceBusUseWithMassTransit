using HepsiExpress.StudyCase.Api.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour
    {
        private readonly RequestDelegate _next;

        public UnhandledExceptionBehaviour(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            ILogger<UnhandledExceptionBehaviour> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("ServiceBusExample Request: {TraceIdentifier} | {Path} | {@User}",
                    context.TraceIdentifier, context.Request.Path,
                    "-1");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = GetStatusCode(ex);

                await JsonSerializer.SerializeAsync(context.Response.Body, ResponseException.Create(ex), null, context.RequestAborted);
            }
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