using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Currency
    {
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
        public Currency(string symbol, decimal rate)
        {
            Symbol = symbol;
            Rate = rate;
        }
    }
}
