using AutoMapper;
using ServiceBusExample.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace ServiceBusExample.Application.Common.Mappings
{
    public interface IEntityMessageModelMap<TMessage, TValue, TEntity>
        where TMessage : class
        where TValue : class
        where TEntity : class//, IEntity
    {
        Func<TMessage, IList<TValue>> Entities { get; }
        Func<TMessage, Guid> MessageId { get; }
        Func<TMessage, DateTime> Timestamp { get; }

        void Mapping(Profile profile)
        {
            var messageMapper = profile
                .CreateMap<TMessage, MessageEntityModel<TEntity>>()
                .ForMember(t => t.Entities, t => t.MapFrom(o => Entities(o)))
                .ForMember(t => t.MessageId, t => t.MapFrom(o => MessageId(o)))
                .ForMember(t => t.Timestamp, t => t.MapFrom(o => Timestamp(o)));

            var valueMapper = profile.CreateMap<TValue, TEntity>();

            //MapMessage(messageMapper);
            MapValue(valueMapper);
        }

        //void MapMessage(IMappingExpression<TMessage, MessageEntityModel<TEntity>> mapper);

        void MapValue(IMappingExpression<TValue, TEntity> mapper);
    }
}