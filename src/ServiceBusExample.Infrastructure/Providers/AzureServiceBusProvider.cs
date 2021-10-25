using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Providers
{
    public class AzureServiceBusProvider : IMessageBrokerProvider
    {
        private readonly string _connectionString;

        public AzureServiceBusProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ServiceBus:ConnectionString");
        }

        public async Task Send<T>(IMessage<T> message, CancellationToken cancellationToken)
             where T : class
        {
            switch (message)
            {
                case IQueueMessage<T> _:
                case IGenericMessage<T> { MessageType: MessageTypes.Queue }:
                    await SendQueue(message);
                    break;

                case ITopicMessage<T> _:
                case IGenericMessage<T> { MessageType: MessageTypes.Topic }:
                    await SendTopic(message);
                    break;

                default:
                    throw new ArgumentException("Wrong message type!");
            }
        }

        private async Task SendTopic<T>(IMessage<T> topic) where T : class
        {
            var topicClient = new TopicClient(_connectionString, topic.Name);
            await topicClient.SendAsync(CreateAzureMessage(topic));
        }

        private async Task SendQueue<T>(IMessage<T> queue) where T : class
        {
            var client = new QueueClient(_connectionString, queue.Name);
            await client.SendAsync(CreateAzureMessage(queue));
        }

        private static Message CreateAzureMessage<T>(IMessage<T> message) where T : class
        {
            var azureMesssage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Body)))
            {
                ContentType = "application/json",
                CorrelationId = message.Id.ToString(),
                SessionId = message.Id.ToString()
            };
            foreach (var item in message.Headers)
            {
                azureMesssage.UserProperties.Add(item.Key, item.Value);
            }
            return azureMesssage;
        }
    }
}