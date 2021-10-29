using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ServiceBusExample.Domain.Models
{
    public abstract class MessageBase<T, TValues> : IMessage<T, TValues>
       where T : class
        where TValues : Dictionary<string, string>
    {
        private MessageBase()
        {
        }

        protected MessageBase(T body, TValues values)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body), "Body can not be null.");
            Values = values ?? throw new ArgumentNullException(nameof(values), "Values can not be null.");
            Headers = values;// SetHeaderContext();// new Dictionary<string, string>;();
        }

        //private Dictionary<string, string> SetHeaderContext()
        //{ 
        //  //  var filteredPropDict = new Dictionary<string, string>();
        //  //  var props =Values.OfType<PropertyInfo>();

        //  //  var subType = Body.GetType()
        //  //      .GetProperty("values", BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

        //  //  var propserer = Body.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.Name == "Values").FirstOrDefault();

        //  //  var _consumerTypes = Body.GetType().GetProperties()
        //  //.Select(t => (Class: t, Attribute: t.GetCustomAttribute<MessageConsumerFilterableAttribute>(true)))
        //  //.ToArray();

        //  //  foreach (var item in props)
        //  //  {
        //  //      var propName =  item.Name;
        //  //      var propValue = (string)item.GetValue(propserer.GetType(),null);//subType.DeclaringType.Name
        //  //      filteredPropDict.Add(item.Name, propValue);
        //  //  }
             

        //    return Values.Values;
        //}

        public virtual Guid Id
            => TryGetIdFromBody(out var id)
            ? id
            : throw new InvalidOperationException($"Message Id must be set on {nameof(T)} - {Body.GetType().FullName}.");

        public abstract string Name { get; }

        public virtual T Body { get; private set; }

        public virtual Dictionary<string, string> Headers { get; set; }

        public virtual TValues Values { get; private set; }

        public abstract Uri GetMessageAddress();

        private bool TryGetIdFromBody(out Guid id)
        {
            id = Guid.Empty;
            var member = Body.GetType()
                .GetProperty("id", BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (member == null) return false;
            id = (Guid)member.GetValue(Body);
            return true;
        }
    }
}