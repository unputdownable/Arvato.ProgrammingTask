using System.Collections.Generic;

namespace Common.Fixer.Models
{
    public class SymbolsResponse : Response
    {
        public IDictionary<string, string> Symbols { get; set; }
    }
}