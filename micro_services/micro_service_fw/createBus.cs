using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace micro_service_fw
{
    public class createBus
    {
        Dictionary<string, string> conf = new Dictionary<string, string>();
        string bus_db_path = string.Empty;
        Share op_share = new Share();

        public createBus()
        {

        }

        public bool AllCreateCore()
        {
            bool result = false;

            //DB ye göre dizinin olup olmadığının kontrolü ve yaratılması
            if (op_share.DirectoryCreate(AppStatic.conf[AppStatic.conf_pathbus], AppStatic.conf[AppStatic.conf_dbname]) == false)
                return result;
            else
                bus_db_path = AppStatic.conf[AppStatic.conf_pathdal] + "\\" + AppStatic.conf[AppStatic.conf_dbname];

            if (originalCreateCore() == false)
                return result;
            else
            {
                if (partialCreateCore() == false)
                    return result;
            }

            result = true;


            return result;
        }

        public bool originalCreateCore()
        {
            bool result = false;

            string pagetop = string.Empty;
            string pagemeth = string.Empty;
            string pagebody = string.Empty;
            string pageline = string.Empty;

            DataTable dt = op_share.mysql_dbtables(AppStatic.conf);

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //using kısmı
                    string tableName = "Tables_in_" + AppStatic.conf[AppStatic.conf_dbname];

                    pagetop = "using System.Collections.Generic;" + Environment.NewLine
                        + "using System.Linq;" + Environment.NewLine
                        + "using micro_services_dal;" + Environment.NewLine
                        + "using micro_services_share;" + Environment.NewLine
                        + "using micro_services_share.vModel;" + Environment.NewLine
                        + "using micro_services_dal.Models." + AppStatic.conf[AppStatic.conf_dbname] + ";" + Environment.NewLine;

                    pagetop += Environment.NewLine;
                    //namespace
                    pagetop += string.Format("namespace micro_services_bus.{0}", AppStatic.conf[AppStatic.conf_dbname]) + Environment.NewLine;
                    //methodlar
                    pagetop += "{" + Environment.NewLine + "@meth" + Environment.NewLine + "}";

                    //Meth
                    pagemeth = string.Format("    public partial class Op_{0}", dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagemeth += "    {" + Environment.NewLine + "@body" + "    }" + Environment.NewLine;

                    //Save
                    pagebody = string.Format("        public {0} Save{0}({0} {1}, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)"
                        , dt.Rows[i][tableName].ToString()
                        , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "        {" + Environment.NewLine;
                    pagebody += string.Format("            {0} result = new {0}();", dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("            BeforeSave{1}({0}: {0}, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);"
                        , dt.Rows[i][tableName].ToString().ToUpper()
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            //eğer birden fazla DataBase güncelleme var ise" + Environment.NewLine;
                    pagebody += "            if (SYNC == true)" + Environment.NewLine;
                    pagebody += string.Format("                {1}.{0}_use = false;"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "            //birden fazla tabloda güncelleme var ise" + Environment.NewLine;
                    pagebody += "            if (TRAN == true)" + Environment.NewLine;
                    pagebody += string.Format("                {1}.{0}_active = false;"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)" + Environment.NewLine;
                    pagebody += "            {" + Environment.NewLine;
                    pagebody += "                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))" + Environment.NewLine;
                    pagebody += "                {" + Environment.NewLine;
                    pagebody += string.Format("                    if ({1}.{0}_id == 0)"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "                    {" + Environment.NewLine;
                    pagebody += "                        long id = 0;" + Environment.NewLine;
                    pagebody += string.Format("                        id = db.Insert<{0}>({1});"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "                        if (id != 0)" + Environment.NewLine;
                    pagebody += string.Format("                            result = db.Get<{0}>(id);"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;

                    pagebody += "                    }" + Environment.NewLine;
                    pagebody += "                    else" + Environment.NewLine;
                    pagebody += "                    {" + Environment.NewLine;
                    pagebody += string.Format("                        bool ok = db.Update<{0}>({1});"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "                        if (ok == true)" + Environment.NewLine;
                    pagebody += string.Format("                            result = db.Get<{0}>({1}.{0}_id);"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "                        else" + Environment.NewLine;
                    pagebody += string.Format("                            result = {0};"
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "                    }" + Environment.NewLine;
                    pagebody += "                }" + Environment.NewLine;
                    pagebody += "            }" + Environment.NewLine;
                    pagebody += string.Format("            AfterSave{0}({1}: {1}, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);"
                            , dt.Rows[i][tableName].ToString()
                            , dt.Rows[i][tableName].ToString().ToUpper()) + Environment.NewLine;
                    pagebody += "            return result;" + Environment.NewLine;
                    pagebody += "        }" + Environment.NewLine;
                    //Delete
                    pagebody += string.Format("        public bool Delete{0}(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "        {" + Environment.NewLine;
                    pagebody += "            bool result = false;" + Environment.NewLine;
                    pagebody += string.Format("            BeforeDelete{0}(ID, ALLOFUSERS, SYNC, TRAN);"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)" + Environment.NewLine;
                    pagebody += "            {" + Environment.NewLine;
                    pagebody += string.Format("                {0} etmp = Get{0}(ID, ALLOFUSERS);"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                //eğer birden fazla Database etkileniyor ise" + Environment.NewLine;
                    pagebody += "                if (SYNC == true)" + Environment.NewLine;
                    pagebody += string.Format("                    etmp.{0}_use = false;"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                //eğer birden fazla tablo etkileniyor ise" + Environment.NewLine;
                    pagebody += "                if (TRAN == true)" + Environment.NewLine;
                    pagebody += string.Format("                    etmp.{0}_active = false;"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("                etmp.deleted{0}_id = true;"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("                {0} eresulttmp = Save{0}(etmp, ALLOFUSERS);"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("                if (eresulttmp.deleted{0}_id == true)"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                    result = true;" + Environment.NewLine;
                    pagebody += "            }" + Environment.NewLine;
                    pagebody += string.Format("            AfterDelete{0}(ID, ALLOFUSERS, SYNC, TRAN);"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            return result;" + Environment.NewLine;
                    pagebody += "        }" + Environment.NewLine;
                    //GET
                    pagebody += string.Format("        public {0} Get{0}(long ID, allofusers ALLOFUSERS, bool ALL=false)"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "        {" + Environment.NewLine;
                    pagebody += string.Format("            {0} result = new {0}();"
                            , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)" + Environment.NewLine;
                    pagebody += "            {" + Environment.NewLine;
                    pagebody += "                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))" + Environment.NewLine;
                    pagebody += "                {";
                    pagebody += string.Format("                    result = db.Get<{0}>(id: ID);"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                    //senkron dışında ve silinenlerin dışındakileri getirmesi" + Environment.NewLine;
                    pagebody += "                    if (ALL==false)" + Environment.NewLine;
                    pagebody += string.Format("                        if ((result.{0}_use == false) || (result.deleted{0}_id == true) || (result.{0}_active==false))"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("                            result = new {0}();"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                }" + Environment.NewLine;
                    pagebody += "            }" + Environment.NewLine;
                    pagebody += "            return result;" + Environment.NewLine;
                    pagebody += "        }" + Environment.NewLine;
                    //GetAll
                    pagebody += string.Format(@"        public List<{0}> GetAll{0}(string whereclause , allofusers ALLOFUSERS, bool ALL=false)"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "        {" + Environment.NewLine;
                    pagebody += string.Format("            List<{0}> result = new List<{0}>();"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format("            BeforeGetAll{0}(whereclause, ALLOFUSERS, ALL);"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            //senkron dışında ve silinenlerin dışındakileri getirmesi" + Environment.NewLine;
                    pagebody += "            if (ALL == false)" + Environment.NewLine;
                    pagebody += "            {" + Environment.NewLine;
                    pagebody += string.Format("                info_{0} info = new info_{0}();"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += string.Format(@"                whereclause += ""AND "" + info.{0}_deleted{0}_id + "" = false AND "" + info.{0}_{0}_use + "" = true AND "" + info.{0}_{0}_active + "" = true"";"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            }" + Environment.NewLine;
                    pagebody += "            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)" + Environment.NewLine;
                    pagebody += "            {" + Environment.NewLine;
                    pagebody += "                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))" + Environment.NewLine;
                    pagebody += "                {" + Environment.NewLine;
                    pagebody += string.Format("                    result = db.GetAll<{0}>(whereclause: whereclause).ToList();"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "                }";
                    pagebody += "            }";
                    pagebody += string.Format("            AfterGetAll{0}(whereclause, ALLOFUSERS, ALL);"
                        , dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagebody += "            return result;" + Environment.NewLine;
                    pagebody += "        }" + Environment.NewLine;
                    //partial olanlar
                    pagebody += string.Format("        public void BeforeSave{0}({0} {1}, allofusers ALLOFUSERS, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()
                        , dt.Rows[i][tableName].ToString().ToUpper()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void AfterSave{0}({0} {1}, allofusers ALLOFUSERS, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()
                        , dt.Rows[i][tableName].ToString().ToUpper()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void AfterDelete{0} (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void BeforeDelete{0}(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void BeforeGet{0}(long ID, allofusers ALLOFUSERS, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void AfterGet{0}(long ID, allofusers ALLOFUSERS, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void BeforeGetAll{0}(string whereclause , allofusers ALLOFUSERS, bool ALL ) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        public void AfterGetAll{0}(string whereclause, allofusers ALLOFUSERS, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;

                    //birleştirme ve yazma
                    pagemeth = pagemeth.Replace("@body", pagebody.Replace('@', '{').Replace('$', '}'));
                    pagetop = pagetop.Replace("@meth", pagemeth);
                    pagetop = pagetop.Replace('İ', 'I').Replace('ı','i');

                    string file_path = AppStatic.conf[AppStatic.conf_pathbus] + "\\" + AppStatic.conf[AppStatic.conf_dbname]
                        + "\\Original\\Op_" + dt.Rows[i][tableName].ToString() + ".cs";

                    //eğer eski dosya var ise siler
                    if (File.Exists(file_path) == true)
                        File.Delete(file_path);

                    FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(pagetop);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            return result;
        }

        public bool partialCreateCore()
        {
            bool result = false;

            string pagetop = string.Empty;
            string pagemeth = string.Empty;
            string pagebody = string.Empty;
            string pageline = string.Empty;

            DataTable dt = op_share.mysql_dbtables(AppStatic.conf);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //using kısmı
                    string tableName = "Tables_in_" + AppStatic.conf[AppStatic.conf_dbname];

                    pagetop = "using System.Collections.Generic;" + Environment.NewLine
                        + "using System.Linq;" + Environment.NewLine
                        + "using micro_services_dal;" + Environment.NewLine
                        + "using micro_services_share;" + Environment.NewLine
                        + "using micro_services_dal.Models." + AppStatic.conf[AppStatic.conf_dbname] + ";" + Environment.NewLine;
                    pagetop += Environment.NewLine;
                    //namespace
                    pagetop += string.Format("namespace micro_services_bus.{0}", AppStatic.conf[AppStatic.conf_dbname]) + Environment.NewLine;
                    //methodlar
                    pagetop += "{" + Environment.NewLine + "@meth" + Environment.NewLine + "}";

                    //Meth
                    pagemeth = string.Format("    public partial class Op_{0}", dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagemeth += "    {" + Environment.NewLine + "@body" + "    }" + Environment.NewLine;

                    //Save
                    //partial olanlar
                    pagebody = string.Format("        //public void BeforeSave{0}({0} {1}, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()
                        , dt.Rows[i][tableName].ToString().ToUpper()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void AfterSave{0}({0} {1}, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()
                        , dt.Rows[i][tableName].ToString().ToUpper()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void AfterDelete{0} (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void BeforeDelete{0}(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void BeforeGet{0}(long ID, string DBTYPE, string CONNSTR, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void AfterGet{0}(long ID, string DBTYPE, string CONNSTR, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void BeforeGetAll{0}(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;
                    pagebody += string.Format("        //public void AfterGetAll{0}(string whereclause, string DBTYPE, string CONNSTR, bool ALL) "
                        , dt.Rows[i][tableName].ToString()) + "{ " + "}" + Environment.NewLine;

                    //birleştirme ve yazma
                    pagemeth = pagemeth.Replace("@body", pagebody.Replace('@', '{').Replace('$', '}'));
                    pagetop = pagetop.Replace("@meth", pagemeth);
                    pagetop = pagetop.Replace('İ', 'I').Replace('ı', 'i');

                    string file_path = AppStatic.conf[AppStatic.conf_pathbus] + "\\" + AppStatic.conf[AppStatic.conf_dbname]
                        + "\\Op_" + dt.Rows[i][tableName].ToString() + ".cs";

                    //eğer eski dosya var ise siler
                    if (File.Exists(file_path) == true)
                        File.Delete(file_path);

                    FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(pagetop);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            return result;
        }
    }
}
