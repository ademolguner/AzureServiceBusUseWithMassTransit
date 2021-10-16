using ServiceBusExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Domain.Interfaces
{
    public interface IGenericMessage<out T> : IMessage<T> where T : class
    {
        MessageTypes MessageType { get; }
    }
}
