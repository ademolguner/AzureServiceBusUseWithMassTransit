using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Models
{
    public class MessageModel<TValue> : IMessageBody
    {
        public IEnumerable<TValue> Values { get; set; }
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
