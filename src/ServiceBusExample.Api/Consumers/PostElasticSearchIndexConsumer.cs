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
    

    [MessageConsumer(MessageTypes.Topic, MessageConsts.PostCreate, MessageConsts.SubscritionName)]
    public class PostElasticSearchIndexConsumer : IConsumer<CreatedPostEventValue>
    {
        public Task Consume(ConsumeContext<CreatedPostEventValue> context)
        {
            // buradaki iş kuralı değişkenlik gösterebilir. Ben Post Id ile  post bilgilerini çekip
            // elasticsearch ile nasıl bir mapping yapıp indexleneceği değişkenlik gösterir

            throw new NotImplementedException();
        }
    }
}
