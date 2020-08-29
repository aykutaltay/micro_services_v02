using System;
using System.Collections.Generic;
using System.Text;
using DapperExtensions;

namespace micro_services_dal
{
    public class Mysql_dapper:Mysql_dapper_abstract
    {
        public Mysql_dapper(string connstr="Server", bool usetransaction=false):base(CONNstr:connstr)
        {

        }

    }
}
