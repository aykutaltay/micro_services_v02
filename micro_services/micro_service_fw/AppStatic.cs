using System;
using System.Collections.Generic;
using System.Text;

namespace micro_service_fw
{
    public static class AppStatic
    {
        public static string conf_dbserver = "DATABASESERVER";
        public static string conf_dbname = "DATABASENAME";
        public static string conf_dbuser = "DBUSERNAME";
        public static string conf_dbuserpass = "DBPASS";
        public static string conf_fwtype = "FWTYPE";
        public static string conf_pathbus = "BUSPATH";
        public static string conf_pathdal = "DALPATH";
        public static string conf_appname = "APPNAME";
        public static Dictionary<string, string> conf = new Dictionary<string, string>();
    }
}
