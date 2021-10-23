using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Others.Mailing.Dtos;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Events.Domain.MailSending;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using System.Collections.Generic;
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


        public async Task<ArticleMailSenBeforeDeliveryOutput> Handle(ArticleMailSenBeforeDeliveryInput request, CancellationToken cancellationToken)
        {
            var mailDto = new SendingMailDto { To = GetMailList(), MailBody = "Body", BodyIsHtml = true, Title = "Maile baslık bulamadım" };
            await _mediator.Publish(notification: new SendingMailCreateEvent(mailDto), cancellationToken);
            return new ArticleMailSenBeforeDeliveryOutput();
        }



        private static string[] GetMailList()
        {
            return new string[] { "ademolguner@gmail.com", "adem.olguner@hepsiburada.com" };
        }
    }
}
