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
        public long tokensofusers_tokensofusers_id { get; set; }
        public long tokensofusers_tokensofusers_users_id { get; set; }
        public string tokensofusers_tokensofusers_token { get; set; }
        public DateTime tokensofusers_tokensofusers_createtime { get; set; }
        public DateTime tokensofusers_tokensofusers_expiretime { get; set; }
        public DateTime tokensofusers_tokensofusers_refreshtime { get; set; }
        public bool tokensofusers_deletedtokensofusers_id { get; set; }
        public bool tokensofusers_tokensofusers_active { get; set; }
        public bool tokensofusers_tokensofusers_use { get; set; }
    }

}
