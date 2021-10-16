using Serilog.Formatting.Elasticsearch;
using System;

namespace ServiceBusExample.Api.Common
{
    public class CustomElasticsearchJsonFormatter : ElasticsearchJsonFormatter
    {
        public CustomElasticsearchJsonFormatter(
            bool omitEnclosingObject = false,
            string closingDelimiter = null,
            bool renderMessage = true,
            IFormatProvider formatProvider = null,
            ISerializer serializer = null,
            bool inlineFields = false,
            bool renderMessageTemplate = false,
            bool formatStackTraceAsArray = false)
            : base(
                omitEnclosingObject,
                closingDelimiter,
                renderMessage,
                formatProvider,
                serializer,
                inlineFields,
                renderMessageTemplate,
                formatStackTraceAsArray)
        {
        }
    }
}
