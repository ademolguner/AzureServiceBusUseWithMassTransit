using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using NewRelic.Api.Agent;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Providers
{
    public class MasstransitProvider : IMessageBrokerProvider
    {
        private readonly ISendEndpointProvider _endpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public MasstransitProvider(
            ISendEndpointProvider endpointProvider,
            IPublishEndpoint publishEndpoint)
        {
            _endpointProvider = endpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        [Trace]
        public async Task Send<T>(IMessage<T> message, CancellationToken cancellationToken) where T : class
        {
            switch (message)
            {
                case IQueueMessage<T> _:
                case IGenericMessage<T> { MessageType: MessageTypes.Queue }:
                    await SendToQueue(message, cancellationToken);
                    break;

                case ITopicMessage<T> _:
                case IGenericMessage<T> { MessageType: MessageTypes.Topic }:
                    await SendToTopic(message, cancellationToken);
                    break;

                default:
                    throw new ArgumentException("Wrong message type!");
            }
        }

        private async Task SendToTopic<T>(IMessage<T> topic, CancellationToken cancellationToken) where T : class
        {
            await _publishEndpoint.Publish(topic.Body, SetContextSettings(topic), cancellationToken);
        }

        private async Task SendToQueue<T>(IMessage<T> queue, CancellationToken cancellationToken) where T : class
        {
            var endPoint = await _endpointProvider.GetSendEndpoint(queue.GetMessageAddress());
            await endPoint.Send(queue.Body, SetContextSettings(queue), cancellationToken);
        }

        private static Action<SendContext<T>> SetContextSettings<T>(IMessage<T> message) where T : class
        {
            return context =>
            {
                context.ConversationId = message.Id;
                context.SetSessionId(message.Id.ToString());

                foreach (var item in message.Headers)
                {
                    context.Headers.Set(item.Key, item.Value);
                }
            };
        }
    }
}