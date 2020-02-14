using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public static class MailService
    {
        public static void SendEmailVerification(string toEmail,string subject,string body)
        {         
            MailMessage mailMessage = new MailMessage("RushHourManager@gmail.com", toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();          
            smtp.Send(mailMessage);
        }
    }
}
