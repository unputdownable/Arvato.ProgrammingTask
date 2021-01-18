using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class Extensions
    {
        public static UriBuilder AddQueryParam(this UriBuilder builder, string name, string value)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[name] = value;
            builder.Query = query.ToString();
            return builder;
        }
    }
}
