using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace micro_services_share
{
    public static class AppStaticMethod
    {
        public static bool Cont_Injektion(string bilgi)
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

        public static string strEncrypt(string sifre)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sifre));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public static string strDecrypt(string SifrelenmisDeger)
        {
            byte[] data = Convert.FromBase64String(SifrelenmisDeger);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(SifrelenmisDeger));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }

        public static void ActivateMailSend(string usermail, string namesurname, string activekey)
        {
            string url = AppStaticStr.urlMailActive + "\\" + activekey;
            List<MailAddress> l_mailadress = new List<MailAddress>();
            MailAddress e_mailadress = new MailAddress(usermail);
            l_mailadress.Add(e_mailadress);

            string str_subject = AppStaticStr.str_ActivateMailSubject;
            string str_body = string.Format(@"
            <head>
              <title>ZorAdamlar Aktivasyon</title>
              <meta charset=""utf - 8"">
              <meta name = ""viewport"" content = ""width=device-width, initial-scale=1"">

              <link rel = ""stylesheet"" href = ""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
              <script src = ""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"" ></script>
              <script src = ""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"" ></script>
              <script src = ""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"" ></script>

            </head >

            <body>

              <link rel = ""stylesheet"" href = ""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
              <script src = ""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"" ></script>
              <script src = ""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"" ></script>
              <script src = ""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"" ></script>

            <form>
            <h2> ZorAdamlar.com yazılım geliştirme platformuna </h2>
            <h2 style = ""text-align:right"" > Hoş geldiniz.</h2>
            <h2> Sayın : {0}</h2>
             <p> </p>
             <p> ZorAdamlar.com platformunda geliştirilen yazılımları kullanmak için açtığınız bu kullanıcının çalışır duruma gelmesi için aşağıda belirtilen ""Etkinleştir"" butonuna basmanız yada açık olarak yazılan adresi kopyalıyarak, kullanıdığınız web browser'a yapıştırmanız yeterli olacaktır.</p>
               <p> Bu işlem sonrasında kullanıcınız sistem üzerinde aktif olacaktır.</p>
                 <p> &nbsp;</p>
                    <a href = ""{1}"" class=""btn btn-primary"">Etkinleştir</a>
            <p>&nbsp;</p>
            <p> Link : <p>
            <div class=""form-group"">
            <label>{1}</label>
            </div>
            </form>
            </body >

            ", namesurname
            , url);


            //string str_body = string.Format(@"
            //<p> ZorAdamlar.com platformunda geliştirilen yazılımları kullanmak için açtığınız bu kullanıcının çalışır duruma gelmesi için aşağıda belirtilen ""Etkinleştir"" butonuna basmanız yada açık olarak yazılan adresi kopyalıyarak, kullanıdığınız web browser'a yapıştırmanız yeterli olacaktır.</p>
            //<p> Bu işlem sonrasında kullanıcınız sistem üzerinde aktif olacaktır.</p>

            //                ");

            MailSend_General(to: l_mailadress, subject: str_subject, body: str_body,isBodyHtml:true);

        }

        public static void MailSend_General(List<MailAddress> to, string subject, string body, string cc = null, SortedList<string, Attachment> files = null, bool isBodyHtml = true)
        {
            MailAddress fromAddress = new MailAddress(AppStaticStr.str_MailActivateFromMail, AppStaticStr.str_MailActivateFrom);
            MailAddress CC = null;
            if (!String.IsNullOrEmpty(cc))
                CC = new MailAddress(cc, "To CC");

            SmtpClient smtp = new SmtpClient
            {
                Host = AppStaticStr.str_MailHostName,
                Port = AppStaticStr.int_MailSMTPPort,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(AppStaticStr.str_MailActivateFromMail, AppStaticStr.str_MailActivateFromMailPass),
            };

            using (MailMessage message = new MailMessage() { From = fromAddress, IsBodyHtml = isBodyHtml, Subject = subject, Body = body })
            {
                for (int i = 0; i < to.Count; i++)
                    message.To.Add(to[i]);

                if (CC != null)
                    message.CC.Add(CC);

                if (files != null && files.Count > 0)
                    foreach (Attachment item in files.Values)
                        message.Attachments.Add(item);

                smtp.Send(message);
            }
        }

    }
}
