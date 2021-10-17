using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Posts.Dtos
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
    }
}
