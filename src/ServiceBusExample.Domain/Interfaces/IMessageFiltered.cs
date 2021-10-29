using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IMessageFiltered<TFilter> where TFilter : class, new()
    {
        Dictionary<string, string> GetFiltered(TFilter value);
    }
}