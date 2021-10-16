using ServiceBusExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Providers
{
    public interface IMessageBrokerProvider
    {
        Task Send<T>(IMessage<T> message, CancellationToken cancellationToken) where T : class;
    }
}