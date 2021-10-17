using AutoMapper;
using MediatR;
using ServiceBusExample.Application.Business.Categories.Queries;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Application.Repositories;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Categories.Commands
{
    public class DeleteCategoryCommandInput : IRequest<DeleteCategoryCommandOutput>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommandOutput
    {
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandInput, DeleteCategoryCommandOutput>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMediator _mediator; 

        public DeleteCategoryCommandHandler(IMapper mapper, IRepositoryContext repositoryContext, IMediator mediator)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _mediator = mediator; 
        }

        public async Task<DeleteCategoryCommandOutput> Handle(DeleteCategoryCommandInput request, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetCategoryQueryInput { CategoryId = request.Id }, cancellationToken);
            if (item.GetCategoryResultDto == null)
                throw new NullReferenceException(message: $"{request.Id} id ile bir Category bulunamadı!");
            var model = _mapper.Map<Category>(item.GetCategoryResultDto);
            _repositoryContext.CategoryRepository.Delete(model.Id);
           
            // ek işlemler yapılabilir. Article pasife cek
            return new DeleteCategoryCommandOutput { };
        }
    }
}
