using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Application.Business.Others.Mailing.Dtos
{
    public class MailSendTemplateDto
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
