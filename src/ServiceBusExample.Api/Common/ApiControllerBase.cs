using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Application.Common.Providers;

namespace ServiceBusExample.Api.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMessageBrokerProvider _messageBrokerProvider;
        private IMediator _mediator;

        protected IMessageBrokerProvider MessageBroker
            => _messageBrokerProvider ??= HttpContext.RequestServices.GetService<IMessageBrokerProvider>();

        protected IMediator Mediator
            => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}