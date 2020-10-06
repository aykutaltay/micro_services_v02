using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share.vModel
{
    public class mainuserinfo
    {
        public long userID { get; set; }
        public int statuValue { get; set; }
        public string authValue { get; set; }
        public long langValue { get; set; }
        public string userName { get; set; }
        public string userMail { get; set; }
        public string userBackupMail { get; set; }
        public string pass { get; set; }
        public string passRepat { get; set; }
        public bool Core_project { get; set; }
        public bool Fason_project { get; set; }
        public string createTime { get; set; }
        public string changeTime { get; set; }
        public string expireTime { get; set; }
    }
}
