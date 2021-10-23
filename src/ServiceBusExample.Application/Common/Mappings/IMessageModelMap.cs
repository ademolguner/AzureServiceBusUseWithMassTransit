using AutoMapper;
using ServiceBusExample.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace ServiceBusExample.Application.Common.Mappings
{
    public interface IMessageModelMap<TMessage, TValue>
        where TMessage : class
        where TValue : class
    {
        Func<TMessage, IEnumerable<TValue>> Values { get; }
        Func<TMessage, Guid> MessageId { get; }
        Func<TMessage, DateTime> Timestamp { get; }

        void Mapping(Profile profile)
        {
            MapMessageModel(profile);
        }

        void MapMessageModel(Profile profile)
        {
            profile.CreateMap<TMessage, MessageModel<TValue>>()
                .ForMember(t => t.Values, t => t.MapFrom(o => Values(o)))
                .ForMember(t => t.Id, t => t.MapFrom(o => MessageId(o)))
                .ForMember(t => t.Timestamp, t => t.MapFrom(o => Timestamp(o)));
        }
    }
}