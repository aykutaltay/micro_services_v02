using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_parameters
    {
        public parameters Saveparameters(parameters PARAMETERS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            parameters result = new parameters();
            BeforeSaveparameters(PARAMETERS: PARAMETERS, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PARAMETERS.parameters_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PARAMETERS.parameters_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
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
            AfterSaveparameters(PARAMETERS: PARAMETERS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteparameters(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteparameters(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                parameters etmp = Getparameters(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.parameters_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.parameters_active = false;
                etmp.deletedparameters_id = true;
                parameters eresulttmp = Saveparameters(etmp, ALLOFUSERS);
                if (eresulttmp.deletedparameters_id == true)
                    result = true;
            }
            AfterDeleteparameters(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public parameters Getparameters(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            parameters result = new parameters();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<parameters>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.parameters_use == false) || (result.deletedparameters_id == true) || (result.parameters_active==false))
                            result = new parameters();
                }
            }
            return result;
        }
        public List<parameters> GetAllparameters(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<parameters> result = new List<parameters>();
            BeforeGetAllparameters(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_parameters info = new info_parameters();
                whereclause += " AND " + info.parameters_deletedparameters_id + " = false AND " + info.parameters_parameters_use + " = true AND " + info.parameters_parameters_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<parameters>(whereclause: whereclause).ToList();
                }            }            AfterGetAllparameters(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveparameters(parameters PARAMETERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveparameters(parameters PARAMETERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteparameters (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteparameters(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetparameters(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetparameters(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllparameters(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllparameters(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
