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
        public long company_company_id { get; set; }
        public string company_company_name { get; set; }
        public DateTime company_company_createtime { get; set; }
        public DateTime company_company_expiretime { get; set; }
        public long company_company_dbserver_id { get; set; }
        public long company_company_appserver_id { get; set; }
        public DateTime company_company_updatetime { get; set; }
        public bool company_deletedcompany_id { get; set; }
        public bool company_company_active { get; set; }
        public bool company_company_use { get; set; }
    }

}
