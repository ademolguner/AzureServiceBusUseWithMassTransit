using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Linq;

namespace ServiceBusExample.Domain.Models
{
    public class GenericMessage<T> : MessageBase<T>, IGenericMessage<T> where T : class
    {
        private readonly string _Name;

        public MessageTypes MessageType { get; }

        public GenericMessage(T body) : base(body)
        {
            var attribute = typeof(T)
                .GetCustomAttributes(typeof(MessageNameAttribute), false)
                .Cast<MessageNameAttribute>()
                .FirstOrDefault();
            if (attribute == null)
                throw new InvalidOperationException($"{body.GetType().Name} must have MessageNameAttribute.");
            MessageType = attribute.MessageType;
            _Name = attribute.Name;
        }

        public override string Name => _Name;

        public override Uri GetMessageAddress()
        {
            return new Uri($"{MessageType.ToString().ToLower()}:{Name}");
        }
    }

    public static class GenericMessage
    {
        public static GenericMessage<T> Create<T>(T body) where T : class => new(body);
    }
}