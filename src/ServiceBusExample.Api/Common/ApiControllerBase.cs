using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBusExample.Api.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator GetMediator()
        {
            return _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}