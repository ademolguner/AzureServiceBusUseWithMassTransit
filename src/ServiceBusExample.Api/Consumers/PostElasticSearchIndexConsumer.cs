using MassTransit;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Consumers
{
    

    [MessageConsumer(MessageTypes.Topic, MessageConsts.ElasticIndex, MessageConsts.SubscritionName,"ElasticSearch")]
    public class PostElasticSearchIndexConsumer : IConsumer<MailSendEventValue>
    {
        public Task Consume(ConsumeContext<MailSendEventValue> context)
        {
            throw new NotImplementedException();
        }
    }
}
