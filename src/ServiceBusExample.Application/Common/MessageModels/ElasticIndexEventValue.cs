using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Topic, MessageConsts.PostCreate)]
    public class ElasticIndexEventValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public IEnumerable<ElasticIndexEventValues> Values { get; set; }
    }

    public class ElasticIndexEventValues
    {
        public string Id { get; set; }
    }
}
