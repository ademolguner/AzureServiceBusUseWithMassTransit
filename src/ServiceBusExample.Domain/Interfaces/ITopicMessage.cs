using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface ITopicMessage<out T, TValues> : IMessage<T, TValues>
        where T : class
        where TValues : Dictionary<string, string>
    {
    }
}