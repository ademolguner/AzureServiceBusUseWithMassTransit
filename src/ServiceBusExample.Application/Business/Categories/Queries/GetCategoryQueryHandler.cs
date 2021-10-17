using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Business.Categories.Dtos;
using ServiceBusExample.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Categories.Queries
{
    public class GetCategoryQueryInput : IRequest<GetCategoryQueryOutput>
    {
        public long CategoryId { get; set; }
    }

    public class GetCategoryQueryOutput
    {
        public CategoryDto GetCategoryResultDto { get; set; }
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryInput, GetCategoryQueryOutput>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IMediator _mediator; 

        public GetCategoryQueryHandler(IMapper mapper, IRepositoryContext repositoryContext, IMediator mediator)
        {
            _mapper = mapper;
            _repositoryContext = repositoryContext;
            _mediator = mediator;
        }

        [Obsolete]
        public async Task<GetCategoryQueryOutput> Handle(GetCategoryQueryInput request, CancellationToken cancellationToken)
        {
            var query = _repositoryContext.CategoryRepository.GetAll().Where(c => c.Id == request.CategoryId);
            var model =  await query.FirstOrDefaultAsync(cancellationToken);
            return new GetCategoryQueryOutput
            {
                GetCategoryResultDto = _mapper.Map<CategoryDto>(model)
            };
        }
    }
}
