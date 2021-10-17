using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Topic, MessageConsts.CategoryDelete)]
    public class CategoryDeleteEventValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public IEnumerable<CategoryDeleteEventValues> Values { get; set; }
    }

    public class CategoryDeleteEventValues
    {
        public string Id { get; set; }
    }
}
