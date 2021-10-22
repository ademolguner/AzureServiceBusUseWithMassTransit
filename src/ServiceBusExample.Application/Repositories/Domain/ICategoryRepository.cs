using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NewRelic.Api.Agent;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace ServiceBusExample.Application.Repositories.Domain
{
    public interface ICategoryRepository : IRepository<Category>
    {
        
    }

    
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(IMasstransitExampleDbContext dbContext)
            : base(dbContext as DbContext)
        {
        }

       
        
    }
}
