using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Articles.Dtos;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Models;

namespace ServiceBusExample.Application.Business.Domain.Articles.Commands
{
    public class CreateArticleCommandInput : IRequest<CreateArticleCommandOutput>
    {
        public ArticleDto CreateArticleDto { get; set; }
    }

    public class CreateArticleCommandOutput
    {
        public ArticleDto ArticleDto { get; set; }
    }

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommandInput, CreateArticleCommandOutput>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMessageBrokerProvider _messageBrokerProvider;

        public CreateArticleCommandHandler(
            IMapper mapper,
            IRepositoryContext repositoryContext,
            IMessageBrokerProvider messageBrokerProvider)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _messageBrokerProvider = messageBrokerProvider;
        }

        public async Task<CreateArticleCommandOutput> Handle(CreateArticleCommandInput request, CancellationToken cancellationToken)
        {
            var articleModel = _mapper.Map<Article>(request.CreateArticleDto);
            var createdArticle = await _repositoryContext.ArticleRepository.AddItem(articleModel, cancellationToken);

            var eventModel = new CreatedArticleEventValue()
            {
                Timestamp = DateTime.Now,
                Id = Guid.NewGuid(),
                Values = new List<CreatedArticleEventValues>
                {
                    new CreatedArticleEventValues()
                    {
                        Id = createdArticle.Id, 
                        Baslik = createdArticle.Title, 
                        Aciklama = createdArticle.Description, 
                        IsIndex = createdArticle.IsIndex
                    }
                }
            };
            
            // await _messageBrokerProvider.Send(message: GenericMessage.Create(body:eventModel, default(Dictionary<string,string>)),cancellationToken: cancellationToken);
             
           await _messageBrokerProvider.Send(
              message: GenericMessage.Create(
                         body:eventModel, 
                         eventModel.GetFiltered(eventModel.Values[0])), 
              cancellationToken);
           
            return new CreateArticleCommandOutput
            {
                ArticleDto = _mapper.Map<ArticleDto>(createdArticle)
            };
        }
    }
}
