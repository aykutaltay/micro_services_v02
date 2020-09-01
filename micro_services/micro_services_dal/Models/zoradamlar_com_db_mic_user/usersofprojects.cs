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
        public long usersofprojects_usersofprojects_id { get; set; }
        public long usersofprojects_usersofprojects_projects_id { get; set; }
        public long usersofprojects_usersofprojects_users_id { get; set; }
        public bool usersofprojects_usersofprojects_active { get; set; }
        public bool usersofprojects_usersofprojects_use { get; set; }
    }

}
