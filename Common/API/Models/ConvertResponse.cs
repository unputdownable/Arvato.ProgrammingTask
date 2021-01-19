using System;

namespace Common.API.Models
{
    public class ConvertResponse
    {
        public Amount From { get; set; }
        public Amount To { get; set; }
        public DateTime Date { get; set; }

        public class Amount
        {
            public string Currency { get; set; }
            public decimal Value { get; set; }
        }
    }
}
