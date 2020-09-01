using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share
{
    public static class AppStaticStr
    {
        public static string msg0001WrongUserNamePass = "Yanlış Kullanıcı Kodu yada Şifre";


        public static string core_dbname = "zoradamlar_com_db_mic_user";
        public static string core_uid = "zorad_AdminUOAA";
        public static string core_pass = "ul@$@ykut2020";
        public static string core_dbTypeMYSQL = "MYSQL";
        public static string core_dbConnStr = string.Format("Server=zoradamlar.com;Database={0};Uid={1};Pwd={2};"
            , core_dbname, core_uid,core_pass);

        public static string Key_username = "USERNAME";
        public static string Key_password = "PASSWORD";

    }
}
