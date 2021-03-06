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
                mail.From = new MailAddress("email soporte", "Sistema de Lavandería - Soporte");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("email soporte", "password");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                mail.Dispose();
                smtp.Dispose();
            }
            catch
            {
                throw new Exception("The password recovery email could not be sent. Please contact the system administrator.");
            }
        }
    }
}
