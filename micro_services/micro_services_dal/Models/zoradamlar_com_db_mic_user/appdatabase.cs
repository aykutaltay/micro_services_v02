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
        public long appdatabase_id { get; set; }
        public string appdatabase_name { get; set; }
        public string appdatabase_type { get; set; }
        public string appdatabase_connstr { get; set; }
        public bool deletedappdatabase_id { get; set; }
        public bool maintenanceappdatabase { get; set; }
        public bool appdatabase_active { get; set; }
        public bool appdatabase_use { get; set; }
    }

}
