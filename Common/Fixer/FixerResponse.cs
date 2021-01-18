using Common.Fixer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Fixer
{
    public class FixerResponse<T> where T : IResponse
    {
        public bool Success { get; set; }
        public T Content { get; set; }
        public Error Error { get; set; }
    }
}
