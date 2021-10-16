using System;
using System.Collections.Generic;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IMessage<out T> where T : class
    {
        T Body { get; }
        Guid Id { get; }
        string Name { get; }
        Dictionary<string, string> Headers { get; }

        Uri GetMessageAddress();
    }
}