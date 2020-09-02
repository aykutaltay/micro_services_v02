using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("tokensofusers")]
    public class tokensofusers
    {
        [DC.Key]
        public long tokensofusers_id { get; set; }
        public long tokensofusers_users_id { get; set; }
        public string tokensofusers_token { get; set; }
        public DateTime tokensofusers_createtime { get; set; }
        public DateTime tokensofusers_expiretime { get; set; }
        public DateTime tokensofusers_refreshtime { get; set; }
        public bool deletedtokensofusers_id { get; set; }
        public bool tokensofusers_active { get; set; }
        public bool tokensofusers_use { get; set; }
    }

}
