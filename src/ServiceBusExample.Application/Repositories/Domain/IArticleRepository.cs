using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Repositories.Domain
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<Article> AddItem(Article articleModel, CancellationToken cancellationToken);
    }

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(IMasstransitExampleDbContext dbContext)
            : base(dbContext as DbContext)
        {
        }

        public async Task<Article> AddItem(Article articleModel, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Article>().AddAsync(articleModel, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return articleModel;
        }
    }
}