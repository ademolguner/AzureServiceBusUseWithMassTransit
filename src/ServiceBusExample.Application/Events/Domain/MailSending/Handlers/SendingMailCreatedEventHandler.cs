using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ServiceBusExample.Application.Events.Domain.MailSending.Handlers
{
    public class SendingMailCreatedEventHandler : INotificationHandler<SendingMailCreateEvent>
    {
        private readonly ILogger<SendingMailCreatedEventHandler> _logger;

        public SendingMailCreatedEventHandler(ILogger<SendingMailCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendingMailCreateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bu alanda mail gönderimi ile ilgili iş kuralını yazabiliriz.");
            return Task.FromResult("Mail gönderimi yapıldı");
        }
    }
}