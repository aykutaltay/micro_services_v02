using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("users")]
    public class users
    {
        [DC.Key]
        public long users_users_id { get; set; }
        public string users_users_name { get; set; }
        public string users_users_mail { get; set; }
        public long users_users_company_id { get; set; }
        public long users_users_role_id { get; set; }
        public DateTime users_users_createtime { get; set; }
        public DateTime users_users_expiretime { get; set; }
        public DateTime users_users_updatetime { get; set; }
        public long users_users_lang_id { get; set; }
        public bool users_deletedusers_id { get; set; }
        public bool users_users_active { get; set; }
        public bool users_users_use { get; set; }
        public string users_users_backupmail { get; set; }
    }

}
