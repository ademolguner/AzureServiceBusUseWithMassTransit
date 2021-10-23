using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Articles.Dtos;
using ServiceBusExample.Application.Business.Others.Mailing.Dtos;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Domain.Articles.Queries
{

    public class ArticleMailSenBeforeDeliveryInput : IRequest<ArticleMailSenBeforeDeliveryOutput>
    {
        public IEnumerable<Article> Articles { get; set; }
    }

    public class ArticleMailSenBeforeDeliveryOutput
    {
        public MailSendTemplateDto MailSendTemplateDto { get; set; }
    }




    public class ArticleMailSenBeforeDeliveryHandler : IRequestHandler<ArticleMailSenBeforeDeliveryInput, ArticleMailSenBeforeDeliveryOutput>
    {

        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMediator _mediator;
        private readonly IMessageBrokerProvider _messageBrokerProvider;

        public ArticleMailSenBeforeDeliveryHandler(IMapper mapper, IRepositoryContext repositoryContext, IMediator mediator, IMessageBrokerProvider messageBrokerProvider)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _mediator = mediator;
            _messageBrokerProvider = messageBrokerProvider;
        }


        public Task<ArticleMailSenBeforeDeliveryOutput> Handle(ArticleMailSenBeforeDeliveryInput request, CancellationToken cancellationToken)
        {



            return Task.FromResult<ArticleMailSenBeforeDeliveryOutput>(null);
           
        }
    }
}
