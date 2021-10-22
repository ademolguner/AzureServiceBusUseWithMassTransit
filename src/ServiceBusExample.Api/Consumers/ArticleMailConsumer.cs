using AutoMapper;
using MassTransit;
using MediatR;
using ServiceBusExample.Application.Business.Articles.Dtos;
using ServiceBusExample.Application.Business.Domain.Articles.Queries;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Extensions;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Consumers
{

    [MessageConsumer(MessageTypes.Topic, MessageConsts.ArticleCreate, MessageConsts.SubscritionName)]
    public class ArticleMailConsumer : IConsumer<CreatedArticleEventValue>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ArticleMailConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreatedArticleEventValue> context)
        {
            await _mediator.Send(
                new ArticleMailSenBeforeDeliveryInput
                    { 
                        CreateArticleDto = _mapper.Map<CreateArticleDto>(context.Message.Values) 
                    }
              , context.CancellationToken);
        }
    }
}
