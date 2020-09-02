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
        public long useractivation_id { get; set; }
        public long useractivation_user_id { get; set; }
        public DateTime useractivation_createtime { get; set; }
        public string useractivation_code { get; set; }
        public bool deleteduseractivation_id { get; set; }
        public bool useractivation_active { get; set; }
        public bool useractivation_use { get; set; }
    }

}
