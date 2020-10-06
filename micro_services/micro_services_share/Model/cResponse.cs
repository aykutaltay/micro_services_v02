using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share.Model
{
    public class cResponse
    {
        public string token { get; set; }
        public int message_code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }
}
