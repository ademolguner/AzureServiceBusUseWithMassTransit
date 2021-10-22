using System;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IMessageBody
    {
        Guid Id { get; }
    }
}