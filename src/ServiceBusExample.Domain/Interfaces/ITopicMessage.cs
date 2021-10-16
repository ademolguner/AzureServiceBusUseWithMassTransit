namespace ServiceBusExample.Domain.Interfaces
{
    public interface ITopicMessage<out T> : IMessage<T> where T : class
    {
    }
}