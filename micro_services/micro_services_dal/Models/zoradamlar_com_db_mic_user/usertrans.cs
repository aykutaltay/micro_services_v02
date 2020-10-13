using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("usertrans")]
    public class usertrans
    {
        [DC.Key]
        public long usertrans_id { get; set; }
        public long usertrans_usersofprojects_id { get; set; }
        public bool usertrans_begin { get; set; }
        public DateTime usertrans_lastbegin { get; set; }
        public bool usertrans_commit { get; set; }
        public DateTime usertrans_lastcommit { get; set; }
        public bool usertrans_rollback { get; set; }
        public DateTime usertrans_lastrollback { get; set; }
        public bool deletedusertrans_id { get; set; }
        public bool usertrans_active { get; set; }
        public bool usertrans_use { get; set; }
    }

}
