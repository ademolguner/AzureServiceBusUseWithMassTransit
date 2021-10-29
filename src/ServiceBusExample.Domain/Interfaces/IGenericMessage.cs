using ServiceBusExample.Domain.Enums;
using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IGenericMessage<out T, TValues> : IMessage<T, TValues> 
        where T : class 
        where TValues :  Dictionary<string, string>
    {
        MessageTypes MessageType { get; }
    }
}