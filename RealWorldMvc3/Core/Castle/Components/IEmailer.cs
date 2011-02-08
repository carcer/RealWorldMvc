using System;
using System.Net.Mail;

namespace RealWorldMvc3.Core.Castle.Components
{
    public interface IEmailer
    {
        void Send(MailMessage message);
    }

    public class SmtpEmailer : IEmailer
    {
        public void Send(MailMessage message)
        {
            throw new InvalidOperationException();
        }
    }

    public class AmazonSimpleEmailer : IEmailer
    {
        public void Send(MailMessage message)
        {
            throw new InvalidOperationException();
        }
    }

    public class OffLineEmailer : IEmailer
    {
        public void Send(MailMessage message)
        {
            
        }
    }
}