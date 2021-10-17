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
    public interface IPostRepository : IRepository<Post>
    {
        
    }

    
    public class PostRepository : Repository<Post>, IPostRepository
    {

        public PostRepository(IMasstransitExampleDbContext dbContext)
            : base(dbContext as DbContext)
        {
        }

       
        
    }
}
