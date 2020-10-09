using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share.Model
{
    public class cRequest
    {
        public string token { get; set; }
        public int project_code { get; set; }
        public int prosses_code { get; set; }
        public string data { get; set; }
        public List<ex_data> data_ex { get; set; }
    }

    public class ex_data
    {
        public int id { get; set; }
        public string info { get; set; }
        public string value { get; set; }
    }
}
