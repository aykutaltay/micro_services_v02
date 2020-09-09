using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DapperExtensions;

namespace micro_services_dal
{
    public class Mysql_dapper : Mysql_dapper_abstract
    {
        public Mysql_dapper(string connstr = "Server", bool usetransaction = false) : base(CONNstr: connstr)
        {

        }

        public bool MultiTableRecordActive(string tables, string values)
        {
            bool result = false;
            string[] TABLES = tables.Split(',');
            string[] VALUES = values.Split(',');

            string sql = string.Empty;

            for (int i = 0; i < TABLES.Count(); i++)
            {
                sql += string.Format("Update {0} set deleted{0}_id=0 Where {0}_id={1};", TABLES[i], VALUES[i]) + Environment.NewLine;
            }

            try
            {
                Execute(sql);
                result = true;
            }
            catch (Exception)
            {


            }

            return result;

        }

    }
}
