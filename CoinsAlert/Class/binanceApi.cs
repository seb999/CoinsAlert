using CoinsAlert.TransferClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoinsAlert.Class
{
    public class BinanceApi
    {
        public static List<BinanceCoin> GetData(Uri api)
        {
            string serializedData = GetApiData(api);
            return JsonConvert.DeserializeObject<binanceCoinTransfer>(serializedData).symbols;
        }

        private static string GetApiData(Uri ApiUri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = ApiUri;

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }

            return "";
        }
    }
}
