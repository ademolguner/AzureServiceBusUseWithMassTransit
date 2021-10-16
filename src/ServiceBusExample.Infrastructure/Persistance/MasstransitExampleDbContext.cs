using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Persistance
{
    public class MasstransitExampleDbContext : DbContext,IMasstransitExampleDbContext
    {

        
        public MasstransitExampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
