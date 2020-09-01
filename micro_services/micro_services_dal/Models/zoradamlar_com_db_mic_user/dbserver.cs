using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("dbserver")]
    public class dbserver
    {
        [DC.Key]
        public long dbserver_dbserver_id { get; set; }
        public string dbserver_dbserver_name { get; set; }
        public string dbserver_dbserver_adrr { get; set; }
        public bool dbserver_deleteddbserver_id { get; set; }
        public bool dbserver_maintenancedbserver { get; set; }
        public bool dbserver_dbserver_active { get; set; }
        public bool dbserver_dbserver_use { get; set; }
    }

}
