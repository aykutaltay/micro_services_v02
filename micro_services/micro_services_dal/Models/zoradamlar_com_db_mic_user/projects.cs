using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("projects")]
    public class projects
    {
        [DC.Key]
        public long projects_id { get; set; }
        public string projects_name { get; set; }
        public string projects_type { get; set; }
        public long projects_appdatabase_id { get; set; }
        public bool deletedprojects_id { get; set; }
        public bool maintenanceprojects { get; set; }
        public bool projects_active { get; set; }
        public bool projects_use { get; set; }
    }

}
