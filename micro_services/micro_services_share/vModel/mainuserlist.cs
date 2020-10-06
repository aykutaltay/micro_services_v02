using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share.vModel
{
    public class mainuserlist
    {
        public long Id { get; set; }
        public string Durum { get; set; }
        public string Yetki { get; set; }
        public string Kod { get; set; }
        public string Email { get; set; }
        public string SonTarih { get; set; }
    }
}
