using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinsAlert.Model
{
    //[Table("currency")]
    public class KuCoin
    {
        public int Id { get; set; }
        public string kuCoinName { get; set; }
        public string kuCoinCoin { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
