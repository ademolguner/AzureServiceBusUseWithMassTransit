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

            var filterItemList = new Dictionary<string, string>();

            foreach (var prop in properties)
            {
                filterItemList.Add(prop.Name, value.GetType().GetRuntimeProperty(prop.Name)?.GetValue(value).ToString());
            }
            return filterItemList;
        }
    }
}
