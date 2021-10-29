using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Domain.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]

    public sealed class MessageConsumerFilterableAttribute : Attribute
    {
        public MessageConsumerFilterableAttribute()
        {
        }
    }
}
