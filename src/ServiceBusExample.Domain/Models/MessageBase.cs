using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Domain.Models
{
    public abstract class MessageBase<T> : IMessage<T> where T : class
    {
        private MessageBase()
        {
        }

        protected MessageBase(T body)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body), "Body can not be null.");
            Headers = new Dictionary<string, string>();
        }

        public virtual Guid Id
            => TryGetIdFromBody(out var id)
            ? id
            : throw new InvalidOperationException($"Message Id must be set on {nameof(T)}.");

        public abstract string Name { get; }

        public virtual T Body { get; private set; }

        public virtual Dictionary<string, string> Headers { get; private set; }

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
