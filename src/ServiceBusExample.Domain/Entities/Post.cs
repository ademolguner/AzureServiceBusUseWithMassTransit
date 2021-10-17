using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Domain.Entities
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
