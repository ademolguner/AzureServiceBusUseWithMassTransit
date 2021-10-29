using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ServiceBusExample.Domain.Models
{
    public class QueueMessage<T, TValues> : MessageBase<T, TValues>, IQueueMessage<T, TValues>
        where T : class
        where TValues : Dictionary<string, string>
    {
        protected string QueueName;

        public QueueMessage(T body, TValues values) : base(body, values)
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