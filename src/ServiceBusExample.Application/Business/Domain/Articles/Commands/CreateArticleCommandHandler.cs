using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Articles.Dtos;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Articles.Commands
{
    public class CreateArticleCommandInput : IRequest<CreateArticleCommandOutput>
    {
        public CreateArticleDto CreateArticleDto { get; set; }
    }

    public class CreateArticleCommandOutput
    {
        public ArticleDto ArticleDto { get; set; }
    }

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommandInput, CreateArticleCommandOutput>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMediator _mediator;
        private readonly IMessageBrokerProvider _messageBrokerProvider;

        public CreateArticleCommandHandler(IMapper mapper, IRepositoryContext repositoryContext, IMediator mediator, IMessageBrokerProvider messageBrokerProvider)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _mediator = mediator;
            _messageBrokerProvider = messageBrokerProvider;
        }

        public async Task<CreateArticleCommandOutput> Handle(CreateArticleCommandInput request, CancellationToken cancellationToken)
        {
            var articleModel = _mapper.Map<Article>(request.CreateArticleDto);
            var createdArticle = await _repositoryContext.ArticleRepository.InsertAsync(articleModel, cancellationToken);

            var createdArticleEventModel = new CreatedArticleEventValue()
            {
                Timestamp = DateTime.Now,
                Id = Guid.NewGuid(),
                Values = new List<CreatedArticleEventValues> { new CreatedArticleEventValues() { Article = createdArticle.Entity } }
            };

            // servicebus send işlemi
            await _messageBrokerProvider.Send(GenericMessage.Create(createdArticleEventModel), cancellationToken);
             
            return new CreateArticleCommandOutput
            {
                ArticleDto = _mapper.Map<ArticleDto>(createdArticle)
            };
        }
    }
}
