using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBusExample.Domain.Models
{
    public abstract class MessageFilteredBase<TFilter> : IMessageFiltered<TFilter> where TFilter : class, new()
    {
        public Dictionary<string, string> GetFiltered(TFilter value)
        {
            var properties = typeof(TFilter)
                .GetProperties()
                .Where(p => p.IsDefined(typeof(MessageConsumerFilterableAttribute), true));

            return properties.ToDictionary(
                prop => prop.Name, 
                prop => value.GetType().GetRuntimeProperty(prop.Name)?.GetValue(value)?.ToString());
        }
    }
}