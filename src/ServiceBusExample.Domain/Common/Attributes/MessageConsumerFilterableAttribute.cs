using System;

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