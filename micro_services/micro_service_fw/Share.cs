using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace micro_service_fw
{
    public class Share
    {
        public bool DirectoryCreate(string direct, string name)
        {
            bool result = false;

            if (Directory.Exists(direct)==true)
            {
                direct = direct + "\\" + name;
                if (Directory.Exists(direct)==true)
                    result = true;
                else
                {
                    Directory.CreateDirectory(direct);
                    result = true;
                }   

            }


            return result;

        }

        public DataTable mysql_dbtables(Dictionary<string,string> conf)
        {
            DataTable dt = new DataTable();

            string connstr = string.Format("server={0};database={1};uid={2};password={3};charset=utf8;port=3306;"
                , AppStatic.conf[AppStatic.conf_dbserver]
                ,AppStatic.conf[AppStatic.conf_dbname]
                ,AppStatic.conf[AppStatic.conf_dbuser]
                ,AppStatic.conf[AppStatic.conf_dbuserpass]);

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES"))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                conn.Close();
            }


            return dt;
        }

        public DataTable mysql_dbtables_field (Dictionary<string, string> conf, string tablename)
        {
            DataTable dt = new DataTable();

            string connstr = string.Format("server={0};database={1};uid={2};password={3};charset=utf8;port=3306;"
                , AppStatic.conf[AppStatic.conf_dbserver]
                , AppStatic.conf[AppStatic.conf_dbname]
                , AppStatic.conf[AppStatic.conf_dbuser]
                , AppStatic.conf[AppStatic.conf_dbuserpass]);

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(string.Format("select * from {0} LIMIT 10",tablename)))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }


            return dt;
        }
    }
}
