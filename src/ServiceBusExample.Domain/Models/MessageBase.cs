using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ServiceBusExample.Domain.Models
{
    public abstract class MessageBase<T, TValues> : IMessage<T, TValues>
       where T : class
        where TValues : Dictionary<string, string>
    {
        private MessageBase()
        {
        }

        protected MessageBase(T body, TValues values)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body), "Body can not be null.");
            Headers = values;
        }

        public virtual Guid Id
            => TryGetIdFromBody(out var id)
            ? id
            : throw new InvalidOperationException($"Message Id must be set on {nameof(T)} - {Body.GetType().FullName}.");

        public abstract string Name { get; }

        public virtual T Body { get; private set; }

        public virtual Dictionary<string, string> Headers { get; set; }

        public virtual TValues? Values { get; private set; }

        public abstract Uri GetMessageAddress();

        private bool TryGetIdFromBody(out Guid id)
        {
            id = Guid.Empty;
            var member = Body.GetType()
                .GetProperty("id", BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (member == null) return false;
            id = (Guid)member.GetValue(Body);
            return true;
        }
    }
}