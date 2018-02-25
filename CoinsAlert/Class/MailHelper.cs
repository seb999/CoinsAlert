using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoinsAlert.Class
{
    public static class MailHelper
    {
        public static void SendEmail(string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //From
                mail.From = new MailAddress(Properties.Settings.Default.emailSender);

                //To
                foreach (var item in Properties.Settings.Default.emailList.Split(';'))
                {
                    if (item == "") continue;
                    mail.To.Add(new MailAddress(item));
                }

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body + "<p>&copy; 2018 - CoinsAlert - Southern Blocks</p>";

                SmtpClient smtp = new SmtpClient(Properties.Settings.Default.SmtpServer, 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Properties.Settings.Default.emailSender, Properties.Settings.Default.emailSenderPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Console.WriteLine("mail sent to subscribers");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendEmailTest(string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //From
                mail.From = new MailAddress(Properties.Settings.Default.emailSender);

                //To
                mail.To.Add(new MailAddress("sebastien.dubos@ecdc.europa.eu"));

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body + "<p>&copy; 2018 - CoinsAlert - Southern Blocks</p>";

                SmtpClient smtp = new SmtpClient(Properties.Settings.Default.SmtpServer, 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Properties.Settings.Default.emailSender, Properties.Settings.Default.emailSenderPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Console.WriteLine("mail sent to subscribers");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
