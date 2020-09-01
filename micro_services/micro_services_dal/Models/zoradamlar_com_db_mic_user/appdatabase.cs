using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("appdatabase")]
    public class appdatabase
    {
        [DC.Key]
        public long appdatabase_appdatabase_id { get; set; }
        public string appdatabase_appdatabase_name { get; set; }
        public string appdatabase_appdatabase_type { get; set; }
        public string appdatabase_appdatabase_connstr { get; set; }
        public bool appdatabase_maintenanceappdatabase { get; set; }
        public bool appdatabase_appdatabase_active { get; set; }
        public bool appdatabase_appdatabase_use { get; set; }
    }

}
