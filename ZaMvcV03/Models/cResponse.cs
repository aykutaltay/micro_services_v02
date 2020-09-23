using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaMvcV03.Models
{
    public class cResponse
    {
        public string token { get; set; }
        public int message_code { get; set; }
        public string message { get; set; }
        public string data { get; set; }

    }
}
