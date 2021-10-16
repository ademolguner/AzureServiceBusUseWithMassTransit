using MassTransit.Topology;
using ServiceBusExample.Domain.Common.Attributes;
using System.Reflection;

namespace ServiceBusExample.Infrastructure.Persistance.Configurations
{
    public class MessageNameFormatter : IEntityNameFormatter
    {
        private readonly IEntityNameFormatter entityNameFormatter;

        public MessageNameFormatter(IEntityNameFormatter entityNameFormatter)
        {
            this.entityNameFormatter = entityNameFormatter;
        }

        public string FormatEntityName<T>()
        {
            var targetType = typeof(T);
            var messageName = targetType.GetCustomAttribute<MessageNameAttribute>();
            if (messageName == null)
            {
                return entityNameFormatter.FormatEntityName<T>();
            }
            return messageName.Name;
        }
    }
}