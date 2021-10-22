using ServiceBusExample.Domain.Interfaces;
using System;

namespace ServiceBusExample.Domain.Models
{
    public class QueueMessage<T> : MessageBase<T>, IQueueMessage<T> where T : class
    {
        protected string QueueName;

        public QueueMessage(T body) : base(body)
        {
        }

        public void SetName(string name) => QueueName = name;

        public override string Name => QueueName;

        public override Uri GetMessageAddress()
        {
            return new Uri($"queue:{Name}");
        }
    }
}