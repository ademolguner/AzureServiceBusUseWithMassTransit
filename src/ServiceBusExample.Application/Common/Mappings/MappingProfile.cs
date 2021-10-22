using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapTypes = new[]
            {
                typeof(IMap),
                typeof(IMap<,>),
                typeof(IMapTo<>),
                typeof(IMapFrom<>),
                typeof(IMapBoth<>),
                typeof(IMapBoth<,>),
                typeof(IMessageModelMap<,>),
                typeof(IEntityMessageModelMap<,,>)
            };
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                        .Any(i => mapTypes.Contains(i) || i.IsGenericType && mapTypes.Contains(i.GetGenericTypeDefinition())))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping");
                if (methodInfo != null)
                {
                    methodInfo?.Invoke(instance, new object[] { this });
                }
                foreach (var item in type.GetInterfaces())
                {
                    methodInfo = item.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}
