using System;
using System.Collections.Generic;

namespace Common.LatestRate
{
    public class LatestRateResponse
    {
        public bool Success { get; set; }
        public ulong Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public IDictionary<Currency, decimal> Rates { get; set; }
    }

    public record LatestRate(bool Success, ulong Timestamp, Currency Base, DateTime Date, IDictionary<Currency, decimal> Rates);
}