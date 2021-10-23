using ServiceBusExample.Application.Common.Mappings;
using ServiceBusExample.Domain.Entities;

namespace ServiceBusExample.Application.Business.Articles.Dtos
{
    public class ArticleDto : IMapBoth<Article>
    {
        //public long Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}