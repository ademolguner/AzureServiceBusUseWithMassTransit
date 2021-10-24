using MassTransit.Audit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Providers
{
    public class MasstransitAuditStore : IMessageAuditStore
    {
        private readonly ILogger<MasstransitAuditStore> _logger;

        public MasstransitAuditStore(ILogger<MasstransitAuditStore> logger)
        {
            _logger = logger;
        }

        public Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            var json = JsonSerializer.Serialize(message);
            var (msg, fields) = GetMessageWithFields(json, metadata);
            _logger.LogInformation(msg, fields);
            return Task.CompletedTask;
        }

        private static (string, object[]) GetMessageWithFields(string messageJson, MessageAuditMetadata metadata)
        {
            var fields = new List<object>();
            var builder = new StringBuilder();

            builder.Append($"Message {metadata.ContextType}: ");

            fields.Add(metadata.SourceAddress);
            builder.Append("SourceAddress: {SourceAddress}, ");

            fields.Add(metadata.DestinationAddress);
            builder.Append("DestinationAddress: {DestinationAddress}, ");

            fields.Add(metadata.ResponseAddress);
            builder.Append("ResponseAddress: {ResponseAddress}, ");

            fields.Add(metadata.ConversationId);
            builder.Append("ConversationId: {ConversationId}, ");

            fields.Add(metadata.MessageId);
            builder.Append("MessageId: {MessageId}, ");

            fields.Add(metadata.SentTime);
            builder.Append("SentTime: {SentTime}, ");

            fields.Add(metadata.ContextType);
            builder.Append("ContextType: {ContextType}, ");

            var matchedDataDate = Regex.Match(messageJson, @"""(Data)?Date""[: ]+""([0-9 \-]+)?T([0-9:\.]+)?""");
            if (matchedDataDate.Success)
            {
                fields.Add(matchedDataDate.Groups[2].Value);
                builder.Append("DataDate: {DataDate}, ");
            }

            var matchedTimestamp = Regex.Match(messageJson, @"""Timestamp""[: ]+""([0-9 \-\.:T]+)?""");
            if (matchedTimestamp.Success)
            {
                fields.Add(matchedTimestamp.Groups[1].Value);
                builder.Append("Timestamp: {Timestamp}, ");
            }

            fields.Add(messageJson.Length);
            builder.Append("MessageSize: {MessageSize}, ");

            if (metadata.Headers is not null)
            {
                foreach (var item in metadata.Headers)
                {
                    fields.Add(item.Value);
                    builder.Append(item.Key + ": {" + Regex.Replace(item.Key, "[^a-zA-Z0-9_]", "") + "}, ");
                }
            }

            if (metadata.Custom is not null)
            {
                foreach (var item in metadata.Custom)
                {
                    fields.Add(item.Value);
                    builder.Append(item.Key + ": {" + Regex.Replace(item.Key, "[^a-zA-Z0-9_]", "") + "}, ");
                }
            }

            builder.Remove(builder.Length - 2, 2);

            return (builder.ToString(), fields.ToArray());
        }
    }
}
