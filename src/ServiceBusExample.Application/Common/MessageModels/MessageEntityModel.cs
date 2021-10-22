using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.MessageModels
{
    public class MessageEntityModel<TEntity>
    {
        public IList<TEntity> Entities { get; set; }
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
