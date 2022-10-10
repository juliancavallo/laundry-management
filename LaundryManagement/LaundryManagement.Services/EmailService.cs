using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Services
{
    public class EmailService
    {
        public void SendMail(string to, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Session.Settings.EmailSettings.Address, Session.Translations["EmailAddressName"]);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(Session.Settings.EmailSettings.Host, Session.Settings.EmailSettings.Port);
                smtp.Credentials = new NetworkCredential(Session.Settings.EmailSettings.Address, Session.Settings.EmailSettings.ApplicationPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                mail.Dispose();
                smtp.Dispose();
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while sending the mail. Please contact the system administrator.", ex);
            }
        }
    }
}
