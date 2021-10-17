using ServiceBusExample.Domain.Entities;
using System;

namespace ServiceBusExample.Application.Common
{
    public interface IMasstransitExampleDbContext : IDisposable
    {
        public Category Category { get; set; }
        public Post  Post { get; set; }
    }
}