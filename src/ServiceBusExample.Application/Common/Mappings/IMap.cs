using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Mappings
{
    public interface IMap
    {
        void Mapping(Profile profile);
    }

    public interface IMap<TSource, TDest>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(TSource), typeof(TDest));
    }

    public interface IMapBoth<TSource, TDest>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(TSource), typeof(TDest)).ReverseMap();
    }

    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }

    public interface IMapBoth<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T)).ReverseMap();
    }
}
