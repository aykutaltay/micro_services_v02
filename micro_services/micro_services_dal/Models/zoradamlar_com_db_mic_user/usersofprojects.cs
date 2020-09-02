using System;
using System.Collections.Generic;
using System.Text;
using DC = Dapper.Contrib.Extensions;

namespace micro_services_dal.Models.zoradamlar_com_db_mic_user
{
    [DC.Table("usersofprojects")]
    public class usersofprojects
    {
        [DC.Key]
        public long usersofprojects_id { get; set; }
        public long usersofprojects_projects_id { get; set; }
        public long usersofprojects_users_id { get; set; }
        public bool deletedusersofprojects_id { get; set; }
        public bool usersofprojects_active { get; set; }
        public bool usersofprojects_use { get; set; }
    }

}
