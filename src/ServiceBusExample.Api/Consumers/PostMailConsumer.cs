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

    [MessageConsumer(MessageTypes.Topic, MessageConsts.PostCreate, MessageConsts.SubscritionName)]
    public class PostMailConsumer : IConsumer<CreatedPostEventValue>
    {
        private readonly IMediator _mediator;

        public PostMailConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Consume(ConsumeContext<CreatedPostEventValue> context)
        {
            _mediator.Send()
            throw new NotImplementedException();
        }
    }
}
