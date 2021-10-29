using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Topic, MessageConsts.ArticleCreate)]
    public class CreatedArticleEventValue : BaseFiltered<CreatedArticleEventValues>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public List<CreatedArticleEventValues> Values { get; set; }

    }

    public class CreatedArticleEventValues // : BaseFiltered<CreatedArticleEventValues>
    {
        public long Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        [MessageConsumerFilterableAttribute("IsIndex")]
        public string IsIndex { get; set; }

        public Dictionary<string, string> GetFiltered<T>()
        {
            throw new NotImplementedException();
        }
    }

    public interface IFiltered<TFilter> where TFilter : class, new()
    {
        Dictionary<string, string> GetFiltered(TFilter value);
    }


    public abstract class BaseFiltered<TFilter> : IFiltered<TFilter> where TFilter : class, new()
    {
        public Dictionary<string, string> GetFiltered(TFilter value)
        {
            var properties = typeof(TFilter)
                .GetProperties()
                .Where(p => p.IsDefined(typeof(MessageConsumerFilterableAttribute), true));

            var filterItemList = new Dictionary<string, string>();
             
            foreach (var prop in properties)
            {
                filterItemList.Add(prop.Name, value.GetType().GetRuntimeProperty(prop.Name)?.GetValue(value).ToString());
            }
            return filterItemList;
        }
    }
}
