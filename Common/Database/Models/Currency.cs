using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Database.Models
{
    public class Currency
    {
        [Key]
        public string Symbol { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
