using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("company")]
    public class company
    {
        [DC.Key]
        public long company_id { get; set; }
        public string company_name { get; set; }
        public DateTime company_createtime { get; set; }
        public DateTime company_expiretime { get; set; }
        public long company_dbserver_id { get; set; }
        public long company_appserver_id { get; set; }
        public DateTime company_updatetime { get; set; }
        public bool deletedcompany_id { get; set; }
        public bool company_active { get; set; }
        public bool company_use { get; set; }
    }

}
