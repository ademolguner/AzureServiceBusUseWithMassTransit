using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Topic, MessageConsts.PostCreate)]
    public class CreatedPostEventValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public IEnumerable<CreatedPostEventValues> Values { get; set; }

    }

    public class CreatedPostEventValues
    {
        public Post Post { get; set; }
    }
}
