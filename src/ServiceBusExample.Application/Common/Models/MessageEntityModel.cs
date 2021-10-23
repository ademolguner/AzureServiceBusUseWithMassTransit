using System;
using System.Collections.Generic;

namespace ServiceBusExample.Application.Common.Models
{
    public class MessageEntityModel<TEntity>
    {
        public IList<TEntity> Entities { get; set; }
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}