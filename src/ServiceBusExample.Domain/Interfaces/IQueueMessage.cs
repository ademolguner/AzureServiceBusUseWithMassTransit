namespace ServiceBusExample.Domain.Interfaces
{
    public interface IQueueMessage<out T> : IMessage<T> where T : class
    {
    }
}