using ServiceBusExample.Application.Common.Mappings;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Posts.Dtos
{
    public class PostDto : IMapBoth<Post>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
    }
}
