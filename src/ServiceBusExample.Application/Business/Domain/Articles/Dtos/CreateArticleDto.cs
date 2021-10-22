using ServiceBusExample.Application.Common.Mappings;

namespace ServiceBusExample.Application.Business.Articles.Dtos
{
    public class CreateArticleDto : IMapFrom<ArticleDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
    }
}