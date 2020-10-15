using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share
{
    public static class AppStaticStr
    {
        public static string msg0001WrongUserNamePass = "Yanlış Kullanıcı Kodu yada Şifre";
        public static string msg0005CorrectUsernamePass = "OK";
        public static string msg0015SaveUsernamePass = "Yeni Kullanıcı Oluşturuldu, aktivasyon için mailinizi kontrol ediniz.";
        public static string msg0020SaveUsernamePassKayitli = "Girilen bilgilere ilişkin kullanıcı kaydı olduğundan işlem iptal edildi";
        public static string msg0025ActivasyonHatasi ="Aktivasyon Yapılamadı";
        public static string msg0030ActivasyonYapildi = "Kullanici Aktive Edildi";
        public static string msg0035AktivasyonKeyHata = "Aktivasyon Key Hatası";
        public static string msg0040Hata="HATA";
        public static string msg0045OK="OK";
        public static string msg0050UuserofSaved = "Kaydeden Kullanıcı Bulunamadı";
        public static string msg0060UserAllready = "Kullanıcı sistemde kayıtlı";
        public static string msg0070SendDataErr = "Gönderilen Bilgi Hatası";
        public static string msg0080UserNotActivation = "Kullanıcı Aktive edilmemiş";

        public static string strAdmin = "admin";


        public static string sec_JWTClaim = "Bu iş hayalini kurduğumuz iş olabilir belkide burada büyük işler yapacağız yada burada yapamayıp sadece oyalanacağız";

        public static string core_dbname = "zoradamlar_com_db_mic_user";
        public static string core_uid = "zorad_AdminUOAA";
        public static string core_pass = "ul@$@ykut2020";
        public static string core_dbTypeMYSQL = "MYSQL";
        public static string core_dbConnStr = string.Format("Server=zoradamlar.com;Database={0};Uid={1};Pwd={2};"
            , core_dbname, core_uid,core_pass);

        public static string Key_username = "USERNAME";
        public static string Key_password = "PASSWORD";
        public static string Key_namesurname ="NAMESURNAME";

        public static string Prm_DefaultExpire = "userExpireDay_default";

        public static string str_DefaultValuesForNewUser="New User Default";
        public static string str_UserExpireDayDefault="userExpireDay_default";
        public static string str_ActivateMailSubject = "ZorAdamlar platformu için açılan hesaba ilişkin etkinleştirme mailidir.";
        public static string str_MailActivateFrom = "Zoradamlar.com Activate Mail ";
        public static string str_MailActivateFromMail = "activate@zoradamlar.com";
        public static string str_MailActivateFromMailPass = "@CTiv$@damlar312";
        public static string str_MailHostName = "mail.zoradamlar.com";
        public static int int_MailSMTPPort = 587;

        public static string MicroServicesBus ="micro_services_bus";
        public static string MicroServicesDal="micro_services_dal";
        public static string SrvOpt ="srvoptname";
        public static string SrvTable ="tablename";
        public static string SrvUserPass = "userpass";
        public static string SrvSingleCrud = "Single_crud";
        public static string SrvTransCrud="Trans_crud";
        public static string SrvTablePrimaryKey="primarykey";
        public static string DllMicServBus ="micro_services_bus.dll";
        public static string SingleCrudSave ="Save";    //Create,Update
        public static string SingleCrudGet ="Get";      //Read
        public static string SingleCrudGetAll ="GetAll";//read
        public static string SingleCrudGetAll_true = "GetAll_true";//read
        public static string SingleCrudDelete ="Delete";//Delete


        public static string urlMailActive = "https://coreesdef.zoradamlar.com/NonSecureOp/Activesiyon?actkey=";
        public static string urlRestUserinfo = "https://localhost:5001/GateOfNewWorld/userinfo";
        public static string urlRestAuth = "https://localhost:5001/GateOfNewWorld/auth";
        public static string urlRestNuser= "https://localhost:5001/GateOfNewWorld/nuser";
        public static string urlRestMainUserList= "https://localhost:5001/GateOfNewWorld/mainuserlist";
        public static string urlRestMainUserGet= "https://localhost:5001/GateOfNewWorld/mainuserget";
        public static string urlRestNewToken = "https://localhost:5001/GateOfNewWorld/retoken";
        public static string urlRestRefCrud = "https://localhost:5001/GateOfNewWorld/refcrud";
        public static string urlRestRefCrudTrans = "https://localhost:5001/GateOfNewWorld/refcrudtrans";
        public static string urlRestUserActMail = "https://localhost:5001/GateOfNewWorld/sendactivemail";

    }
}
