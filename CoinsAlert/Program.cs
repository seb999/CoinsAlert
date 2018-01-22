
using CoinsAlert.Class;
using CoinsAlert.Model;
using CoinsAlert.TransferClass;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Timers;


namespace CoinsAlert
{
    class Program
    {

        private static System.Timers.Timer dispatcherTimer;
        private static Uri uriKucoin = new Uri("https://api.kucoin.com/v1/market/open/coins");
        private static Uri uriBinance = new Uri("https://api.binance.com/api/v1/exchangeInfo");

        static void Main(string[] args)
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            Console.WriteLine("Timer activated");

            SetTimer();

            //For debugging
            //UpdateDbWithNewCoin();
           // CheckNewKuCoin();
            //CheckNewBinanceCoin();
            //Console.WriteLine(string.Format("Checking new coins now {0}", DateTime.Now));
            Console.ReadLine();
        }

        private static void SetTimer()
        {
            dispatcherTimer = new Timer(Properties.Settings.Default.TimerFrequency);
            dispatcherTimer.Elapsed += DispatcherTimer_Elapsed;
            dispatcherTimer.AutoReset = true;
            dispatcherTimer.Enabled = true;
            dispatcherTimer.Start();
        }

        private static void DispatcherTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(string.Format("Checking new coins now {0}", DateTime.Now));
            UpdateDbWithNewCoin();
            CheckNewKuCoin();
            CheckNewBinanceCoin();
        }

        private static void UpdateDbWithNewCoin()
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.DefaultConnection))
            {
                try
                {
                    AppDbContext myContext = new AppDbContext(connection, false);
                    Database.SetInitializer<AppDbContext>(null);

                    //List of coins saved in db
                    List<KuCoin> dbKuCoinList = myContext.KuCoin.ToList();
                    List<binance> dbBinanceList = myContext.Binance.ToList();

                    //List of coins from ZuCoin and BinanceCoin API
                    List<KuCoinCoin> kuCoinList = KucoinApi.GetZuCoinCoin(uriKucoin);
                    List<BinanceCoin> bianceCoinList = BinanceApi.GetData(uriBinance);

                    //Update KuCoin table
                    foreach (var token in kuCoinList)
                    {
                        if (dbKuCoinList.Where(p => p.kuCoinCoin == token.Coin).Select(p => p.Id).FirstOrDefault() == 0)
                        {
                            myContext.KuCoin.Add(new Model.KuCoin()
                            {
                                kuCoinName = token.Name,
                                kuCoinCoin = token.Coin,
                                DateAdded = DateTime.Now
                            });
                        }
                    }

                    //Update Binance table
                    foreach (var token in bianceCoinList)
                    {
                        if (dbBinanceList.Where(p => p.binanceSymbol == token.Symbol).Select(p => p.Id).FirstOrDefault() == 0)
                        {
                            myContext.Binance.Add(new binance()
                            {
                                binanceSymbol = token.Symbol,
                                binanceBaseAsset = token.BaseAsset,
                                binanceQuoteAsset = token.QuoteAsset,
                                DateAdded = DateTime.Now
                            });
                        }
                    }
                    myContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }

        private static void CheckNewKuCoin()
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.DefaultConnection))
            {
                try
                {
                    AppDbContext myContext = new AppDbContext(connection, false);
                    Database.SetInitializer<AppDbContext>(null);

                    //List new currency that are not older than 2 days 
                    DateTime currentDay = DateTime.Now.AddDays(-2);
                    List<KuCoin> newCoinList = myContext.KuCoin.Where(p => DateTime.Compare(p.DateAdded, currentDay) > 0).Select(p => p).ToList();

                    if (newCoinList.Count() > 0)
                    {
                        string newCoinTemplate = "";
                        foreach (var coin in newCoinList)
                        {
                            newCoinTemplate = newCoinTemplate  + "Coin :" + coin.kuCoinCoin + "<br />Name : " + coin.kuCoinName + "<br /><br />";
                        }
                      // MailHelper.SendEmail("New coins available on KuCoin Exchange", newCoinTemplate);
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CheckNewBinanceCoin()
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.DefaultConnection))
            {
                try
                {
                    AppDbContext myContext = new AppDbContext(connection, false);
                    Database.SetInitializer<AppDbContext>(null);

                    //List new currency that are not older than 10 minutes 
                    DateTime currentDay = DateTime.Now.AddMinutes(-10);
                    List<binance> newCoinList = myContext.Binance.Where(p => DateTime.Compare(p.DateAdded, currentDay) > 0).Select(p => p).ToList();

                    if (newCoinList.Count() > 0)
                    {
                        string newCoinTemplate = "";
                        foreach (var coin in newCoinList)
                        {
                            newCoinTemplate = newCoinTemplate + "Symbol :" + coin.binanceSymbol + "<br />Quote Asset : " + coin.binanceQuoteAsset + "<br />Base Asset : " + coin.binanceBaseAsset + "<br /><br />";
                        }
                        MailHelper.SendEmail("New coins available on Binance Exchange", newCoinTemplate);
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
