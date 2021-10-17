using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Categories.Queries;
using ServiceBusExample.Application.Business.Posts.Dtos;
using ServiceBusExample.Application.Common.MessageModels;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Posts.Commands
{
    public class CreatePostCommandInput : IRequest<CreatePostCommandOutput>
    {
        public CreatePostDto CreatePostDto { get; set; }
    }

    public class CreatePostCommandOutput
    {
        public PostDto PostDto { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandInput, CreatePostCommandOutput>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMediator _mediator;
        private readonly IMessageBrokerProvider _messageBrokerProvider;

        public CreatePostCommandHandler(IMapper mapper, IRepositoryContext repositoryContext, IMediator mediator, IMessageBrokerProvider messageBrokerProvider)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _mediator = mediator;
            _messageBrokerProvider = messageBrokerProvider;
        }

        public async Task<CreatePostCommandOutput> Handle(CreatePostCommandInput request, CancellationToken cancellationToken)
        {
            var PostModel = _mapper.Map<Post>(request.CreatePostDto);
            var categoryHandlerResponse = await _mediator.Send(new GetCategoryQueryInput { CategoryId = request.CreatePostDto.CategoryId }, cancellationToken);
            if (categoryHandlerResponse.GetCategoryResultDto == null)
                throw new NullReferenceException(message: $"{request.CreatePostDto.CategoryId} id ile bir Category bulunamadı!");

            var createdPost = await _repositoryContext.PostRepository.InsertAsync(PostModel, cancellationToken);

            // servicebus send işlemi
            var createdPostEventModel = new CreatedPostEventValue()
            {
                Timestamp = DateTime.Now,
                Id = Guid.NewGuid(),
                Values = new List<CreatedPostEventValues> { new CreatedPostEventValues() { Post = createdPost.Entity } }
            };
            await _messageBrokerProvider.Send(GenericMessage.Create(createdPostEventModel), cancellationToken);



            return new CreatePostCommandOutput
            {
                PostDto = _mapper.Map<PostDto>(createdPost)
            };
        }
    }
}
