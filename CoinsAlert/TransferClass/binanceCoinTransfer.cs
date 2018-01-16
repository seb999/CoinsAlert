using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsAlert.TransferClass
{
    public class BinanceCoin
    {
        public string Symbol { get; set; }
        public string QuoteAsset { get; set; }
        public string BaseAsset { get; set; }
    }

    public class binanceCoinTransfer
    {
        public List<BinanceCoin> symbols { get; set; }
    }
}
