using System;
using System.Collections.Generic;
using System.Linq;
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
                    mail.To.Add(new MailAddress(item));
                }
                
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body + "<p>&copy; 2018 - CoinsAlert - Southern Blocks</p>";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = Properties.Settings.Default.SmtpServer;
                smtp.UseDefaultCredentials = false;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
