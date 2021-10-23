using System;
using System.Linq;

namespace ServiceBusExample.Application.Common.Extensions
{
    public static class EnviromentExtension
    {
        public static string GetLocalStackTrace<T>(this T _) where T : class
            => string.Join('\n', Environment.StackTrace.Split("\n")
                .Skip(2)
                .Where(t => !t.Contains("at System."))
                .Distinct());
    }
}