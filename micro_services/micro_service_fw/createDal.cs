using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace micro_service_fw
{
    public class createDal
    {
        Dictionary<string, string> conf = new Dictionary<string, string>();
        string dal_db_path = string.Empty;
        Share op_share = new Share();
        public createDal()
        {

        }

        public bool AllCreate()
        {
            bool result = false;

            //DB ye göre dizinin olup olmadığının kontrolü ve yaratılması
            if (op_share.DirectoryCreate(AppStatic.conf[AppStatic.conf_pathdal] + "\\Models", AppStatic.conf[AppStatic.conf_dbname]) == false)
                return result;
            else
                dal_db_path = AppStatic.conf[AppStatic.conf_pathdal] + "\\Models\\" + AppStatic.conf[AppStatic.conf_dbname];

            if (infoCreate() == false)
                return result;
            else
            {
                if (entityCreate() == false)
                    return result;
            }

            result = true;



            return result;

        }

        public bool infoCreate()
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
                    string tableName = "Tables_in_" + AppStatic.conf[AppStatic.conf_dbname];
                    pagetop = "using System;" + Environment.NewLine + "using System.Collections.Generic;" + Environment.NewLine + "using System.Text;" + Environment.NewLine;
                    pagetop += Environment.NewLine;
                    pagetop += string.Format("namespace micro_services_dal.Models.{0}", AppStatic.conf[AppStatic.conf_dbname]) + Environment.NewLine;
                    pagetop += "{" + Environment.NewLine + "@meth" + Environment.NewLine + "}";

                    pagemeth = string.Format("    public class info_{0}", dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagemeth += "    {" + Environment.NewLine + "@body" + "    }" + Environment.NewLine;

                    DataTable dt_fields = op_share.mysql_dbtables_field(AppStatic.conf, dt.Rows[i][tableName].ToString());

                    pagebody = string.Empty;
                    for (int k = 0; k < dt_fields.Columns.Count; k++)
                    {
                        pagebody += string.Format(@"        public string {0}_{1} = ""{1}"";"
                            , dt.Rows[i][tableName].ToString()
                            , dt_fields.Columns[k].ColumnName) + Environment.NewLine;
                    }

                    pagebody += string.Format(@"        public string {0}_tablename = ""{0}"";"
                            , dt.Rows[i][tableName].ToString()
                            ) + Environment.NewLine;

                    pagemeth = pagemeth.Replace("@body", pagebody);
                    pagetop = pagetop.Replace("@meth", pagemeth);
                    pagetop = pagetop.Replace('İ', 'I').Replace('ı', 'i');

                    string file_path = AppStatic.conf[AppStatic.conf_pathdal] + "\\Models\\"
                        + AppStatic.conf[AppStatic.conf_dbname] + "\\info_" + dt.Rows[i][tableName].ToString() + ".cs";

                    //eğer eski dosya var ise siler
                    if (File.Exists(file_path) == true)
                        File.Delete(file_path);

                    FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(pagetop);
                    sw.Flush();
                    sw.Close();
                    
                }
                result = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
               
            }

            return result;
        }

        public bool entityCreate()
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
                    //uses kısmı
                    string tableName = "Tables_in_" + AppStatic.conf[AppStatic.conf_dbname];
                    pagetop = "using System;" + Environment.NewLine + "using System.Collections.Generic;" + Environment.NewLine + "using System.Text;" + Environment.NewLine
                        + "using DC = Dapper.Contrib.Extensions;"+Environment.NewLine;
                    pagetop += Environment.NewLine;
                    //namespace
                    pagetop += string.Format("namespace micro_services_dal.Models.{0}", AppStatic.conf[AppStatic.conf_dbname]) + Environment.NewLine;
                    //methodlar
                    pagetop += "{" + Environment.NewLine + "@meth" + Environment.NewLine + "}";

                    pagemeth = string.Format(@"    [DC.Table(""{0}"")]", dt.Rows[i][tableName].ToString())+Environment.NewLine;
                    pagemeth += string.Format("    public class {0}", dt.Rows[i][tableName].ToString()) + Environment.NewLine;
                    pagemeth += "    {" + Environment.NewLine + "@body" + "    }" + Environment.NewLine;

                    DataTable dt_fields = op_share.mysql_dbtables_field(AppStatic.conf, dt.Rows[i][tableName].ToString());

                    pagebody = string.Format("        [DC.Key]")+Environment.NewLine;
                    for (int k = 0; k < dt_fields.Columns.Count; k++)
                    {
                        string dtype_name = string.Empty;
                        
                        if (dt_fields.Columns[k].DataType.Name == "varchar")
                            dtype_name = "string";
                        if (dt_fields.Columns[k].DataType.Name == "Int64")
                            dtype_name = "long";
                        if (dt_fields.Columns[k].DataType.Name == "Int32")
                            dtype_name = "int";
                        if (dt_fields.Columns[k].DataType.Name == "String")
                            dtype_name = "string";
                        if (dt_fields.Columns[k].DataType.Name == "SByte")
                            dtype_name = "bool";
                        if (dt_fields.Columns[k].DataType.Name == "DateTime")
                            dtype_name = "DateTime";
                        if (dt_fields.Columns[k].DataType.Name == "Double")
                            dtype_name = "double";
                        
                        pagebody += string.Format(@"        public {2} {1} @ get; set; $"
                            , dt.Rows[i][tableName].ToString()
                            , dt_fields.Columns[k].ColumnName
                            , dtype_name) + Environment.NewLine;
                    }

                    pagemeth = pagemeth.Replace("@body", pagebody.Replace('@','{').Replace('$','}'));
                    pagetop = pagetop.Replace("@meth", pagemeth);
                    pagetop = pagetop.Replace('İ', 'I').Replace('ı', 'i');

                    string file_path = AppStatic.conf[AppStatic.conf_pathdal] + "\\Models\\"
                        + AppStatic.conf[AppStatic.conf_dbname] + "\\" + dt.Rows[i][tableName].ToString() + ".cs";

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
            catch (Exception)
            {


            }

            return result;
        }






    }
}
