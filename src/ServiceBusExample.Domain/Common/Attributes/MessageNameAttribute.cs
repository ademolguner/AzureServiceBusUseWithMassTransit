using ServiceBusExample.Domain.Enums;
using System;

namespace ServiceBusExample.Domain.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageNameAttribute : Attribute
    {
        private readonly string _name;
        public MessageTypes MessageType { get; }

        public string Name
        {
            get
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    var userName = Environment.GetEnvironmentVariable("WINUSER") ?? Environment.UserName;
                    return $"{userName}/{_name}";
                }
                return _name;
            }
        }

        public MessageNameAttribute(MessageTypes messageType, string name)
        {
            MessageType = messageType;
            _name = name;
        }
    }
}