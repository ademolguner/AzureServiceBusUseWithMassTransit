using ServiceBusExample.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Providers
{
    public interface IMessageBrokerProvider
    {
        Task Send<T, TValues>(IMessage<T, TValues> message, CancellationToken cancellationToken)
          where T : class
          where TValues : Dictionary<string, string>;
    }
}