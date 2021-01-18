using System;
using System.Collections.Generic;

namespace Common.Fixer.Models
{
    public class LatestRatesResponse : Response
    {
        public ulong Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}