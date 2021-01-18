using System;
using System.Collections.Generic;

namespace Common.Fixer.Models
{
    public class HistoricalRatesResponse : Response
    {
        public bool Historical { get; set; }
        public DateTime Date { get; set; }
        public ulong Timestamp { get; set; }
        public string Base { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}