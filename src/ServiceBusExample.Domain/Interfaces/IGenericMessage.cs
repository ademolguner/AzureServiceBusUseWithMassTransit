using ServiceBusExample.Domain.Enums;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IGenericMessage<out T> : IMessage<T> where T : class
    {
        MessageTypes MessageType { get; }
    }
}