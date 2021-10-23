using System.Net.Mail;

namespace ServiceBusExample.Application.Business.Others.Mailing.Dtos
{
    public class SendingMailDto
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