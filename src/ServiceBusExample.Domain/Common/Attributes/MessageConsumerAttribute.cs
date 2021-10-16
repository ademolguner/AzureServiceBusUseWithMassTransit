using ServiceBusExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Domain.Common.Attributes
{
    public class MessageConsumerAttribute : MessageNameAttribute
    {
        public string FilterQuery { get; }
        public string SubscriptionName { get; }

        public MessageConsumerAttribute(MessageTypes messageType, string name, string subscriptionName = null, string filterQuery = null)
            : base(messageType, name)
        {
            if (messageType != MessageTypes.Topic && filterQuery != null)
                throw new ArgumentException($"{nameof(filterQuery)} can only be used for {MessageTypes.Topic}");
            SubscriptionName = subscriptionName;
            FilterQuery = filterQuery;
        }
    }
}
