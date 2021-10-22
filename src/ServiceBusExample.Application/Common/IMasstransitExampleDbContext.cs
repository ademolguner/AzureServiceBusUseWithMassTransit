using ServiceBusExample.Domain.Entities;
using System;

namespace ServiceBusExample.Application.Common
{
    public interface IMasstransitExampleDbContext : IDisposable
    {
        public Article Article { get; set; }
    }
}