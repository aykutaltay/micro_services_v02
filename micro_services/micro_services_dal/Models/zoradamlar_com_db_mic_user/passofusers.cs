using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("passofusers")]
    public class passofusers
    {
        [DC.Key]
        public long passofusers_id { get; set; }
        public long passofusers_users_id { get; set; }
        public string passofusers_pass { get; set; }
        public DateTime passofusers_createtime { get; set; }
        public DateTime passofusers_expiretime { get; set; }
        public bool deletedpassofusers_id { get; set; }
        public bool passofusers_active { get; set; }
        public bool passofusers_use { get; set; }
    }

}
