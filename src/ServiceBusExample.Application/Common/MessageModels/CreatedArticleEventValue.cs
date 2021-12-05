using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Extensions;
using ServiceBusExample.Domain.Models;
using System;
using System.Collections.Generic;
using ServiceBusExample.Domain.Enums;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(MessageTypes.Topic, MessageConsts.ArticleCreate)]
    public class CreatedArticleEventValue : MessageFilteredBase<CreatedArticleEventValues>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public List<CreatedArticleEventValues> Values { get; set; }
    }

    public class CreatedArticleEventValues
    {
        public long Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        [MessageConsumerFilterable]
        public string IsIndex { get; set; }
    }
}