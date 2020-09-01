using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share
{
    public static class AppStaticMethod
    {
        public static bool Cont_Injektion (string bilgi)
        {
            bool result = false;

            bilgi = bilgi.ToUpper();

            if (bilgi.Contains("SELECT") == true)
                return result;
            if (bilgi.Contains("UPDATE") == true)
                return result;
            if (bilgi.Contains("DELETE") == true)
                return result;
            if (bilgi.Contains("INSERT") == true)
                return result;
            if (bilgi.Contains("DROP") == true)
                return result;
            if (bilgi.Contains("EXEC") == true)
                return result;

            result = true;

            return result;
        }
    }
}
