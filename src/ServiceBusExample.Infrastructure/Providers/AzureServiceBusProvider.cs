using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public async Task Send<T, TValues>(IMessage<T, TValues> message, CancellationToken cancellationToken)
             where T : class
             where TValues : Dictionary<string, string>
        {
            switch (message)
            {
                case IQueueMessage<T, TValues> _:
                case IGenericMessage<T, TValues> { MessageType: MessageTypes.Queue }:
                    await SendQueue(message);
                    break;

                case ITopicMessage<T, TValues> _:
                case IGenericMessage<T, TValues> { MessageType: MessageTypes.Topic }:
                    await SendTopic(message);
                    break;

                default:
                    throw new ArgumentException("Wrong message type!");
            }
        }

        private async Task SendTopic<T, TValues>(IMessage<T, TValues> topic) 
            where T : class
             where TValues : Dictionary<string, string>
        {
            var topicClient = new TopicClient(_connectionString, topic.Name);
            await topicClient.SendAsync(CreateAzureMessage(topic));
        }

        private async Task SendQueue<T, TValues>(IMessage<T, TValues> queue)
            where T : class
             where TValues : Dictionary<string, string>
        {
            var client = new QueueClient(_connectionString, queue.Name);
            await client.SendAsync(CreateAzureMessage(queue));
        }

        private static Message CreateAzureMessage<T, TValues>(IMessage<T, TValues> message) 
            where T : class
             where TValues : Dictionary<string, string>
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