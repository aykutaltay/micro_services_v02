using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaMvcV03.Models
{
    public class cRequest
    {
        public string token { get; set; }
        public int project_code { get; set; }
        public int prosses_code { get; set; }
        public string data { get; set; }
        public string data_ex { get; set; }
    }
}
