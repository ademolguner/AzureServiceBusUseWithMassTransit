using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Common.MessageModels
{
    [MessageName(Domain.Enums.MessageTypes.Queue, MessageConsts.MailSend)]
    public class MailSendEventValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public IEnumerable<MailSendEventValues> Values { get; set; }
    }

    public class MailSendEventValues
    {
        public string Title { get; set; }
        public string MailBody { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public Attachment[] Attachments { get; set; }
        public bool BodyIsHtml { get; set; }
    }
}
