using System;
using System.Net.Mail;

namespace PrismInfrastructure.Models
{
    public class Email
    {
        public uint Key { get; set; }
        public MailMessage Message { get; set; }

        public DateTime Date => Convert.ToDateTime(Message.Headers["Date"]);
    }
}
