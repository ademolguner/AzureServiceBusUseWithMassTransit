using MassTransit;
using MediatR;
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

    [MessageConsumer(MessageTypes.Topic, MessageConsts.ArticleCreate, MessageConsts.SubscritionName)]
    public class ArticleMailConsumer : IConsumer<CreatedArticleEventValue>
    {
        private readonly IMediator _mediator;
        public ArticleMailConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Consume(ConsumeContext<CreatedArticleEventValue> context)
        {
            
            throw new NotImplementedException();
        }
    }
}
