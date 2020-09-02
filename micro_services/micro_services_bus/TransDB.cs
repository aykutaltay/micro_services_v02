using micro_services_share;
using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_bus
{
    public class TransDB
    {
        public bool DBCommit(string DBTYPE, string CONNSTR, string users_id, string tables)
        {
            bool result = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                string[] Tables = tables.Split(',');

                //reflection ile her bir tablo önce get all ile çağrılıp , içindeki active ler 1 yapılıp kaydedilmeli

            }

            return result;
        }

        public bool DBRollback(string DBTYPE, string CONNSTR, string users_id, string tables)
        {
            bool result = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                string[] Tables = tables.Split(',');

                //reflection ile her bir tablo önce get all ile çağrılıp , içindeki deleted ler 1 yapılıp kaydedilmeli

            }

            return result;
        }

        public bool SyncCommit()
        {
            //bu projede yada başka server makinelerine aktarım gerekir ise buradan yapılabilir
            bool result = false;
            return result;
        }

        public bool SyncRollback()
        {
            bool result = false;
            return result;
        }



    }
}
