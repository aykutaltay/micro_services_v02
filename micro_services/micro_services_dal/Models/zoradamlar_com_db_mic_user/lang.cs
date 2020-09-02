using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("lang")]
    public class lang
    {
        [DC.Key]
        public long lang_id { get; set; }
        public string lang_name { get; set; }
        public long lang_country_id { get; set; }
        public bool deletedlang_id { get; set; }
        public bool lang_active { get; set; }
        public bool lang_use { get; set; }
    }

}
