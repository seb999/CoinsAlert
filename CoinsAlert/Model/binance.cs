using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsAlert.Model
{
    public class binance
    {
        public int Id { get; set; }
        public string binanceSymbol { get; set; }
        public string binanceBaseAsset { get; set; }
        public string binanceQuoteAsset { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
