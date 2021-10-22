using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Domain.Entities;

namespace ServiceBusExample.Application.Repositories.Domain
{
    public interface IArticleRepository : IRepository<Article>
    {
    }

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(IMasstransitExampleDbContext dbContext)
            : base(dbContext as DbContext)
        {
        }
    }
}