using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace micro_service_fw
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            EntryPrepare();
        }



        public static void EntryPrepare()
        {

            Console.Clear();
            Console.WriteLine("DAL için class lar oluşturulacak");
            Console.WriteLine("Program, config.conf dosayası üzerindeki bilgiler ile işlem yapacaktır");
            Console.WriteLine("İşleme devam etmek için bir tuşa basın");
            Console.ReadLine();
            //dosya okuma ve hafızaya almaİ
            ReadtoConfig();
        bas:
            Console.WriteLine("dal yazarsanız, DataAccessLayer ile ilgili işlemleri yapar, corebus yazarsanız, Core Bus oplarını yapar");
            Console.WriteLine("çıkmak için exit yazınız");
            string secim = Console.ReadLine();
            if (secim.ToUpper() == "DAL")
            {
                if (new createDal().AllCreate() == false)
                    Console.WriteLine("İşlem tamamlanamadı, aktarımda hata oldu");
                else
                    Console.WriteLine("Entityler yazıldı");
            }
            if (secim.ToUpper() == "COREBUS")
            {
                if (new createBus().AllCreateCore() == false)
                    Console.WriteLine("İşlem tamamlanamadı, CORE aktarımında hata oldu");
                else
                    Console.WriteLine("CORE op'ları yazıldı");
            }
            if (secim.ToUpper() == "EXIT")
                goto son;

            Console.Clear();
            goto bas;

            son:
            Console.ReadLine();
        }

        public static void ReadtoConfig()
        {

            int i = 1;

            string filepath = "config.conf";
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] tmp = line.Split('"');
                if (tmp.Count() == 2)
                {

                    if ((tmp[0] == AppStatic.conf_dbname) || (tmp[0] == AppStatic.conf_dbuser)
                        || (tmp[0] == AppStatic.conf_dbuserpass) || (tmp[0] == AppStatic.conf_fwtype)
                        || (tmp[0] == AppStatic.conf_pathbus) || (tmp[0] == AppStatic.conf_pathdal)
                        || (tmp[0] == AppStatic.conf_dbserver))
                    {
                        AppStatic.conf.Add(tmp[0], tmp[1]);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Okunan satır belirlenen değişlenler dışında, satır no:{0}", i));
                    }

                }
                else
                {
                    Console.WriteLine(string.Format("Girilen satır okunamadı, satır no: {0}", i));
                }

                i++;
                line = sr.ReadLine();
            }
            Console.WriteLine("Okunan dosya bilgileri;");
            Console.WriteLine(string.Format("{0}:{1}", AppStatic.conf_dbserver, AppStatic.conf[AppStatic.conf_dbserver]));
            Console.WriteLine(string.Format("{0}:{1}", AppStatic.conf_dbname, AppStatic.conf[AppStatic.conf_dbname]));
            Console.WriteLine(string.Format("{0}:{1}", AppStatic.conf_dbuser, AppStatic.conf[AppStatic.conf_dbuser]));
            Console.WriteLine(string.Format("{0}:{1}", AppStatic.conf_dbuserpass, AppStatic.conf[AppStatic.conf_dbuserpass]));
            Console.WriteLine(string.Format("{0}:{1}", AppStatic.conf_fwtype, AppStatic.conf[AppStatic.conf_fwtype]));
            Console.WriteLine(string.Format("{0}={1}", AppStatic.conf_pathbus, AppStatic.conf[AppStatic.conf_pathbus]));
            Console.WriteLine(string.Format("{0}={1}", AppStatic.conf_pathdal, AppStatic.conf[AppStatic.conf_pathdal]));



        }
    }
}
