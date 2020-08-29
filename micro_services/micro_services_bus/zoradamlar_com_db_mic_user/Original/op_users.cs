using System;
using System.Collections.Generic;
using System.Text;
using micro_services_dal;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
//using Dapper;
//using DapperExtensions;

namespace micro_services_bus.zoradamlar_com_db_mic_user.Original
{
    public partial class op_users
    {
        public users Saveusers(users USERS)
        {
            users result = new users();

            using (Mysql_dapper db = new Mysql_dapper(connstr: "Server", usetransaction: false))
            {
                if (USERS.users_id == 0)
                {
                    long id = 0;
                    id = db.Insert<users>(USERS);
                    if (id != 0)
                        result=db.Get<users>(id);
                }
                else
                {
                    bool ok = db.Update<users>(USERS);
                    if (ok == true)
                        result=db.Get<users>(USERS.users_id);
                    else
                        result=USERS;

                }
            }

            return result;
            
        }
    }
}
