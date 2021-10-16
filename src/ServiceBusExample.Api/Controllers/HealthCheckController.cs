using Microsoft.AspNetCore.Mvc;
using ServiceBusExample.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    public class HealthCheckController : ApiControllerBase
    {
        /// <summary>
        /// Web Api status - HealthCheck Status
        /// </summary>
        [HttpGet]
        [Route("/health/status")]
        public void Status()
        {
        }

        /// <summary>
        /// Status about Web Api - HealthCheck Ready
        /// </summary>
        [HttpGet]
        [Route("/health/ready")]
        public void Ready()
        {
        }
    }
}
