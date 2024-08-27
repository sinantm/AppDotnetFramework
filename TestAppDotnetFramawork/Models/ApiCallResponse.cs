using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppDotnetFramawork.Models
{
    public class ApiCallResponse
    {
        public string StatusCode { get; set; }
        public string Content { get; set; }
    }

    public class RequestHeaderItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
