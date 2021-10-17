using ServiceBusExample.Application.Common.Mappings;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Categories.Dtos
{
    public class CategoryDto : IMapBoth<Category>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
