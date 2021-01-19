using System;
using System.Collections.Generic;

namespace Common.Fixer.Models
{
    public class RatesResponse : Response
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public ulong Timestamp { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}