using ServiceBusExample.Application.Repositories.Domain;
using System;
using System.Collections.Generic;

namespace ServiceBusExample.Application.Repositories
{
    public interface IRepositoryContext
    {
        public IArticleRepository ArticleRepository
          => GetRepository<IArticleRepository>();

        TRepo GetRepository<TRepo>() where TRepo : class;

        object GetRepository(Type target);
    }

    public class RepositoryContext : IRepositoryContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, object> _localRepos;

        public RepositoryContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _localRepos = new Dictionary<Type, object>();
        }

        public TRepo GetRepository<TRepo>()
            where TRepo : class
        {
            var target = typeof(TRepo);
            return (TRepo)GetRepository(target);
        }

        public object GetRepository(Type target)
        {
            if (_localRepos.TryGetValue(target, out var repo))
                return repo;
            repo = _serviceProvider.GetService(target);
            _localRepos.Add(target, repo);
            return repo;
        }
    }
}