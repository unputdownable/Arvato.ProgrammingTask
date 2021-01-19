using System;
using System.Collections.Generic;

namespace Common
{
    public class CurrencyConverter
    {
        public IDictionary<string, decimal> Rates { get; }

        public CurrencyConverter(IDictionary<string, decimal> rates)
        {
            Rates = rates;
        }

        public decimal Convert(string from, string to, decimal amount)
        {
            var rateFrom = GetRate(from);
            var rateTo = GetRate(to);
            return (amount / rateFrom) * rateTo;
        }

        private decimal GetRate(string symbol)
        {
            if (!Rates.TryGetValue(symbol.ToUpper(), out var rate)) throw new Exception("Rate not found");
            return rate;
        }

        
    }
}
