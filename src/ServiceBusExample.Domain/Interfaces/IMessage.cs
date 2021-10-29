using System;
using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IMessage<out T, TValues>
        where T : class
        where TValues :  Dictionary<string, string>
    {
        T Body { get; }
        Guid Id { get; }
        string Name { get; }
        Dictionary<string, string> Headers { get; }

        Uri GetMessageAddress();

        TValues Values { get; }
    }
}