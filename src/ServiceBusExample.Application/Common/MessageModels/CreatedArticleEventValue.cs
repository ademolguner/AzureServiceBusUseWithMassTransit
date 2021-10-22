using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Entities;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Topic, MessageConsts.ArticleCreate)]
    public class CreatedArticleEventValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public IEnumerable<CreatedArticleEventValues> Values { get; set; }
    }

    public class CreatedArticleEventValues
    {
        public Article Article { get; set; }
    }
}