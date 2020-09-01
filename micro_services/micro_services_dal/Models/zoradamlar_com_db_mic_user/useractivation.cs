using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("useractivation")]
    public class useractivation
    {
        [DC.Key]
        public long useractivation_useractivation_id { get; set; }
        public long useractivation_useractivation_user_id { get; set; }
        public DateTime useractivation_useractivation_createtime { get; set; }
        public string useractivation_useractivation_code { get; set; }
        public bool useractivation_useractivation_active { get; set; }
        public bool useractivation_useractivation_use { get; set; }
    }

}
