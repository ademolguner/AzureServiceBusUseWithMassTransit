using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceBusExample.Domain.Models
{
    public class GenericMessage<T, TValues> : MessageBase<T, TValues>, IGenericMessage<T, TValues>
        where T : class
        where TValues : Dictionary<string, string>
    {
        public MessageTypes MessageType { get; }

        public GenericMessage(T body, TValues values = default) : base(body, values)
        {
            var attribute = typeof(T)
                .GetCustomAttributes(typeof(MessageNameAttribute), false)
                .Cast<MessageNameAttribute>()
                .FirstOrDefault();
            if (attribute == null)
                throw new InvalidOperationException($"{body.GetType().Name} must have MessageNameAttribute.");
            MessageType = attribute.MessageType;
            Name = attribute.Name;
        }

        public override string Name { get; }

        public override Uri GetMessageAddress()
        {
            return new Uri($"{MessageType.ToString().ToLower()}:{Name}");
        }
    }

    public static class GenericMessage
    {
        public static GenericMessage<T, TValues> Create<T, TValues>(T body, TValues values = default)
            where T : class
            where TValues : Dictionary<string, string>
        {
            return new GenericMessage<T, TValues>(body, values);
        }
    }
}