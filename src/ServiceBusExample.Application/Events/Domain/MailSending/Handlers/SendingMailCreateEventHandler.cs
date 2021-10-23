using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Events.Domain.MailSending
{
    public class SendingMailCreateEventHandler : INotificationHandler<SendingMailCreateEvent>
    {
        private readonly ILogger<SendingMailCreateEventHandler> _logger;

        public SendingMailCreateEventHandler(ILogger<SendingMailCreateEventHandler> logger)
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