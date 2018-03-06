using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextmagicRest;

namespace CoinsAlert.Class
{
    public class SmsHelper
    {
        public static void SendSms(string messageContent)
        {
            try
            {
                //
                foreach (var phoneNumber in Properties.Settings.Default.phoneList.Split(';'))
                {
                    if (phoneNumber == "") continue;
                    var client = new Client("sebastiendubos", "bvBVqEFSLjnXix6UKv1H6eGc3dkOJ9");
                    var link = client.SendMessage(messageContent, phoneNumber);
                    if (link.Success)
                    {
                        Console.WriteLine("Sms sent to subscribers");
                    }
                    else
                    {
                        Console.WriteLine("Message was not sent due to following exception: {0}", link.ClientException.Message);
                    }
                }        
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
