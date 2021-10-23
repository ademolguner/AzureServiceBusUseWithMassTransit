using MediatR;
using ServiceBusExample.Application.Business.Others.Mailing.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Events.Domain.MailSending
{
    public class SendingMailCreateEvent:INotification
    {
        public SendingMailCreateEvent(SendingMailDto sendingMailDto)
        {
            SendingMailDto = sendingMailDto;

        }
        public SendingMailDto SendingMailDto { get; set; }
    }
}
