using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace micro_services.A00
{
    public class cRequest
    {
        public string token { get; set; }
        public int project_code { get; set; }
        public int prosses_code { get; set; }
        public Object data { get; set; }
        public Object data_ex { get; set; }
    }
}
