using MediatR;
using ServiceBusExample.Application.Business.Others.Mailing.Dtos;

namespace ServiceBusExample.Application.Events.Domain.MailSending
{
    public class SendingMailCreateEvent : INotification
    {
        public SendingMailCreateEvent(SendingMailDto sendingMailDto)
        {
            SendingMailDto = sendingMailDto;
        }

        public SendingMailDto SendingMailDto { get; set; }
    }
}