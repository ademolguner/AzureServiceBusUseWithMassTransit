using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Articles.Dtos;
using ServiceBusExample.Application.Business.Others.Mailing.Dtos;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Domain.Articles.Queries
{

    public class ArticleMailSenBeforeDeliveryQuery : IRequest<ArticleMailSenBeforeDeliveryOutput>
    {
        public CreateArticleDto CreateArticleDto { get; set; }
    }

    public class ArticleMailSenBeforeDeliveryOutput
    {
        public MailSendTemplateDto MailSendTemplateDto { get; set; }
    }




    public class ArticleMailSenBeforeDeliveryHandler : IRequestHandler<ArticleMailSenBeforeDeliveryQuery, ArticleMailSenBeforeDeliveryOutput>
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


        public Task<ArticleMailSenBeforeDeliveryOutput> Handle(ArticleMailSenBeforeDeliveryQuery request, CancellationToken cancellationToken)
        {
            //İş kuralları ile ilgili kod alanı.
            throw new NotImplementedException();
        }
    }
}
