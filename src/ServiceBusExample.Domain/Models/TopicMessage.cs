using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace ServiceBusExample.Domain.Models
{
    public abstract class TopicMessage<T, TValues> : MessageBase<T, TValues>, ITopicMessage<T, TValues>
        where T : class
        where TValues : Dictionary<string, string>
    {
        protected TopicMessage(T body, TValues values) : base(body, values)
        {
        }

        public override Uri GetMessageAddress()
        {
            return new Uri($"topic:{Name}");
        }
    }
}