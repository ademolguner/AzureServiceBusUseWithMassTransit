using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceBusExample.Application.Business.Domain.Articles.Queries;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Consumers
{
    [MessageConsumer(MessageTypes.Topic, MessageConsts.ArticleCreate, MessageConsts.SubscritionName2, " IsIndex LIKE '%evet%'")]
    public class ArticleElasticIndexConsumer : IConsumer<CreatedArticleEventValue>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ArticleElasticIndexConsumer> _logger;
         
        public ArticleElasticIndexConsumer(IMediator mediator, IMapper mapper, ILogger<ArticleElasticIndexConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreatedArticleEventValue> context)
        {
            _logger.LogInformation("Bu alanda mail gönderimi ile ilgili iş kuralını yazabiliriz.");
            var model = _mapper.Map<List<Article>>(context.Message.Values);
            await _mediator.Send(new ArticleMailSenBeforeDeliveryInput { Articles = model }, context.CancellationToken);
        }
    }
}