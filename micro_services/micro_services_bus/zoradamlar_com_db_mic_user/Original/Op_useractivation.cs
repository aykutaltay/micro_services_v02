using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_useractivation
    {
        public useractivation Saveuseractivation(useractivation USERACTİVATİON, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            useractivation result = new useractivation();
            BeforeSaveuseractivation(USERACTİVATİON: USERACTİVATİON,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERACTİVATİON.useractivation_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERACTİVATİON.useractivation_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (USERACTİVATİON.useractivation_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<useractivation>(USERACTİVATİON);
                        if (id != 0)
                            result = db.Get<useractivation>(id);
                    }
                    else
                    {
                        bool ok = db.Update<useractivation>(USERACTİVATİON);
                        if (ok == true)
                            result = db.Get<useractivation>(USERACTİVATİON.useractivation_id);
                        else
                            result = USERACTİVATİON;
                    }
                }
            }
            AfterSaveuseractivation(USERACTİVATİON: USERACTİVATİON, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteuseractivation(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteuseractivation(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                useractivation etmp = Getuseractivation(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.useractivation_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.useractivation_active = false;
                etmp.deleteduseractivation_id = true;
                useractivation eresulttmp = Saveuseractivation(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deleteduseractivation_id == true)
                    result = true;
            }
            AfterDeleteuseractivation(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public useractivation Getuseractivation(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            useractivation result = new useractivation();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<useractivation>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.useractivation_use == false) || (result.deleteduseractivation_id == true) || (result.useractivation_active==false))
                            result = new useractivation();
                }
            }
            return result;
        }
        public List<useractivation> GetAllusers(string whereclause = "1 = 1", string DBTYPE = " ", string CONNSTR = " ", bool ALL=false)
        {
            List<useractivation> result = new List<useractivation>();
            BeforeGetAlluseractivation(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_useractivation info = new info_useractivation();
                whereclause += "AND " + info.useractivation_deleteduseractivation_id + " = false AND " + info.useractivation_useractivation_use + " = true AND " + info.useractivation_useractivation_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<useractivation>(whereclause: whereclause).ToList();
                }            }            AfterGetAlluseractivation(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveuseractivation(useractivation USERACTİVATİON, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveuseractivation(useractivation USERACTİVATİON, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteuseractivation (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteuseractivation(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetuseractivation(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetuseractivation(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAlluseractivation(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAlluseractivation(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
