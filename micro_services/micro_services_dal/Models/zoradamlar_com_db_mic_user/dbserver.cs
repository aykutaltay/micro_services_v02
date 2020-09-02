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
        public long dbserver_id { get; set; }
        public string dbserver_name { get; set; }
        public string dbserver_adrr { get; set; }
        public bool deleteddbserver_id { get; set; }
        public bool maintenancedbserver { get; set; }
        public bool dbserver_active { get; set; }
        public bool dbserver_use { get; set; }
    }

}
