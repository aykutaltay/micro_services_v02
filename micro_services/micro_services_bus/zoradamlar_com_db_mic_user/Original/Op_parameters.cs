using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_parameters
    {
        public parameters Saveparameters(parameters PARAMETERS, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            parameters result = new parameters();
            BeforeSaveparameters(PARAMETERS: PARAMETERS,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PARAMETERS.parameters_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PARAMETERS.parameters_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (PARAMETERS.parameters_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<parameters>(PARAMETERS);
                        if (id != 0)
                            result = db.Get<parameters>(id);
                    }
                    else
                    {
                        bool ok = db.Update<parameters>(PARAMETERS);
                        if (ok == true)
                            result = db.Get<parameters>(PARAMETERS.parameters_id);
                        else
                            result = PARAMETERS;
                    }
                }
            }
            AfterSaveparameters(PARAMETERS: PARAMETERS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteparameters(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteparameters(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                parameters etmp = Getparameters(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.parameters_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.parameters_active = false;
                etmp.deletedparameters_id = true;
                parameters eresulttmp = Saveparameters(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedparameters_id == true)
                    result = true;
            }
            AfterDeleteparameters(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public parameters Getparameters(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            parameters result = new parameters();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<parameters>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.parameters_use == false) || (result.deletedparameters_id == true) || (result.parameters_active==false))
                            result = new parameters();
                }
            }
            return result;
        }
        public List<parameters> GetAllparameters(string whereclause , string DBTYPE , string CONNSTR , bool ALL=false)
        {
            List<parameters> result = new List<parameters>();
            BeforeGetAllparameters(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_parameters info = new info_parameters();
                whereclause += "AND " + info.parameters_deletedparameters_id + " = false AND " + info.parameters_parameters_use + " = true AND " + info.parameters_parameters_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<parameters>(whereclause: whereclause).ToList();
                }            }            AfterGetAllparameters(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveparameters(parameters PARAMETERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveparameters(parameters PARAMETERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteparameters (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteparameters(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetparameters(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetparameters(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllparameters(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllparameters(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
