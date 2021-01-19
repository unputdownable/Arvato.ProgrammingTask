using System;

namespace Common.Database.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ulong Timestamp { get; set; }
        public decimal Value { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
