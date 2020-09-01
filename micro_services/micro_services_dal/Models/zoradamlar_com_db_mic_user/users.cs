using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("users")]
    public class users
    {
        [DC.Key]
        public long users_id { get; set; }
        public string users_name { get; set; }
        public string users_mail { get; set; }
        public long users_company_id { get; set; }
        public long users_role_id { get; set; }
        public DateTime users_createtime { get; set; }
        public DateTime users_expiretime { get; set; }
        public DateTime users_updatetime { get; set; }
        public long users_lang_id { get; set; }
        public bool deletedusers_id { get; set; }
        public bool users_active { get; set; }
        public bool users_use { get; set; }
        public string users_backupmail { get; set; }



    }
}
