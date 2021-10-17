using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Articles.Dtos
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
    }
}
