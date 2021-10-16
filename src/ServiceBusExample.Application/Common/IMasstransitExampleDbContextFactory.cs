﻿using ServiceBusExample.Domain.Common.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common
{
    public interface IMasstransitExampleDbContextFactory : IScopedDependency
    {
        IMasstransitExampleDbContext CreateDbContext();
    }
}