using ServiceBusExample.Domain.Interfaces;
using System;

namespace ServiceBusExample.Domain.Models
{
    public abstract class TopicMessage<T> : MessageBase<T>, ITopicMessage<T> where T : class
    {
        protected TopicMessage(T body) : base(body)
        {
        }

        public override Uri GetMessageAddress()
        {
            return new Uri($"topic:{Name}");
        }
    }
}