using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("country")]
    public class country
    {
        [DC.Key]
        public long country_id { get; set; }
        public string country_name { get; set; }
        public bool deletedcountry_id { get; set; }
        public bool country_use { get; set; }
        public bool country_active { get; set; }
    }

}
