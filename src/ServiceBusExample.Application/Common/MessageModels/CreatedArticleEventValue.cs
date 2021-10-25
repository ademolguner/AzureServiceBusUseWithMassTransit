using ServiceBusExample.Domain.Common.Attributes;
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
        public long Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        [MessageConsumerFilterableAttribute("IsIndex")]
        public string IsIndex { get; set; }
    }
}