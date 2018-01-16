using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsAlert.TransferClass
{
    public class KuCoinCoin
    {
        public string Name { get; set; }
        public string Coin { get; set; }
    }

    public class KuCoinTransfer
    {
        public List<KuCoinCoin> Data { get; set; }
    }
}
