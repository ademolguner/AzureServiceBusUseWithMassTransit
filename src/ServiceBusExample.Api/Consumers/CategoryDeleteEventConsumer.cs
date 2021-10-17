using MassTransit;
using MassTransit.Mediator;
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
    [MessageConsumer(MessageTypes.Topic, MessageConsts.CategoryDelete, MessageConsts.SubscritionName)]
    public class CategoryDeleteEventConsumer : IConsumer<CategoryDeleteEventValue>
    {
        private readonly IMediator _mediator;

        public CategoryDeleteEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Consume(ConsumeContext<CategoryDeleteEventValue> context)
        {
            // category delete işlemi ve sonrasına ait iş kuralları yazılabilir.
            throw new NotImplementedException();
        }
    }
}
