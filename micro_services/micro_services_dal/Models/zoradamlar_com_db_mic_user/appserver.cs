using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("appserver")]
    public class appserver
    {
        [DC.Key]
        public long appserver_id { get; set; }
        public string appserver_name { get; set; }
        public string appserver_addr { get; set; }
        public bool deletedappserver_id { get; set; }
        public bool maintenanceappserver { get; set; }
        public bool appserver_active { get; set; }
        public bool appserver_use { get; set; }
    }

}
