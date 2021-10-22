using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Common
{
    public class VersionHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var ver = Environment.GetEnvironmentVariable("VERSION") ?? "NONE";
            var assembly = Assembly.GetEntryAssembly();
            var assemblyVersion = assembly?.GetName().Version?.ToString();
            var data = new Dictionary<string, object> {
                { "BuildVersion", assemblyVersion },
                { "EnvironmentVariable", ver },
                { "ASPNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "NONE" }
            };
            var result = HealthCheckResult.Healthy("API version info", data);
            return Task.FromResult(result);
        }
    }
}