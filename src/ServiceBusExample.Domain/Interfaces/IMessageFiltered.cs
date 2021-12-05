using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IMessageFiltered<in TFilter> 
        where TFilter : class, new()
    {
        Dictionary<string, string> GetFiltered(TFilter value);
    }
}