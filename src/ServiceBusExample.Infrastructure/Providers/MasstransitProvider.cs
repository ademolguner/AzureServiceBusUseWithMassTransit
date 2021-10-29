using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using NewRelic.Api.Agent;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
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
        public async Task Send<T, TValues>(IMessage<T, TValues> message, CancellationToken cancellationToken)
              where T : class
            where TValues : Dictionary<string, string>
        {
            switch (message)
            {
                case IQueueMessage<T, TValues> _:
                case IGenericMessage<T, TValues> { MessageType: MessageTypes.Queue }:
                    await SendToQueue(message, cancellationToken);
                    break;

                case ITopicMessage<T, TValues> _:
                case IGenericMessage<T, TValues> { MessageType: MessageTypes.Topic }:
                    await SendToTopic(message, cancellationToken);
                    break;

                default:
                    throw new ArgumentException("Wrong message type!");
            }
        }

        private async Task SendToTopic<T, TValues>(IMessage<T, TValues> topic, CancellationToken cancellationToken)
             where T : class
            where TValues : Dictionary<string, string>
        {
            await _publishEndpoint.Publish(topic.Body, SetContextSettings(topic), cancellationToken);
        }

        private async Task SendToQueue<T, TValues>(IMessage<T, TValues> queue, CancellationToken cancellationToken)
            where T : class
            where TValues : Dictionary<string, string>
        {
            var endPoint = await _endpointProvider.GetSendEndpoint(queue.GetMessageAddress());
            await endPoint.Send(queue.Body, cancellationToken);
        }

        private static Action<MassTransit.SendContext<T>> SetContextSettings<T, TValues>(IMessage<T, TValues> message)
           where T : class
            where TValues : Dictionary<string, string>
        {
            return context =>
             {
                 context.ConversationId = message.Id;
                 context.SetSessionId(message.Id.ToString());
                 if (message.Headers != null)
                     foreach (var item in message.Headers)
                     {
                         context.Headers.Set(item.Key, item.Value);
                     }
             };

            throw new NotImplementedException();
        }
    }
}