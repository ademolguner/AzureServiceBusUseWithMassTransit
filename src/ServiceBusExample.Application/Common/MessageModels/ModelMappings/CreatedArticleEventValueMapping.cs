using AutoMapper;
using ServiceBusExample.Application.Common.Mappings;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceBusExample.Application.Common.MessageModels.ModelMappings
{
    public class CreatedArticleEventValueMapping : IEntityMessageModelMap<CreatedArticleEventValue, CreatedArticleEventValues, Article>
    {
        public Func<CreatedArticleEventValue, IList<CreatedArticleEventValues>> Entities => v => v.Values.ToList();
        public Func<CreatedArticleEventValue, Guid> MessageId => v => v.Id;
        public Func<CreatedArticleEventValue, DateTime> Timestamp => v => v.Timestamp;

        //public void MapMessage(IMappingExpression<CreatedArticleEventValue, MessageEntityModel<Article>> mapper)
        //{
        //    mapper.ReverseMap();
        //    //mapper.ForMember(t => t.Entities, t => t.MapFrom(o => Entities(o)))
        //    //    .ForMember(t => t.MessageId, t => t.MapFrom(o => MessageId(o)))
        //    //    .ForMember(t => t.Timestamp, t => t.MapFrom(o => Timestamp(o)));
        //}

        public void MapValue(IMappingExpression<CreatedArticleEventValues, Article> mapper)
        {
            mapper.ForMember(t => t.Description, t => t.MapFrom(o => o.Aciklama))
                .ForMember(t => t.Title, t => t.MapFrom(o => o.Baslik))
                .ForMember(t => t.Id, t => t.MapFrom(o => o.Id));
        }
    }
}