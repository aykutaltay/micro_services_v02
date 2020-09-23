using micro_services.A00;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_bus.zoradamlar_com_db_mic_user;
using MySqlX.XDevAPI.Common;
using micro_services_share.vModel;
using micro_services_bus;
using System.Net.Http;
using micro_services.A00_Model;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

//https://jasonwatmore.com/post/2020/05/25/aspnet-core-3-api-jwt-authentication-with-refresh-tokens
//yukarıdaki sayfa referans alınmış olup refresh token ve revoke yapılmadan kullanılmıştır.

namespace micro_services.A00_Core
{
    public class classToken
    {
        NSOperation Op_ns = new NSOperation();
        Op_tokensofusers Op_tokenofUsers = new Op_tokensofusers();
        info_tokensofusers info_tokenofUsers = new info_tokensofusers();
        Op_core Op_Core = new Op_core();

        public cResponse Authenticate(cRequest model, string ipAddress)
        {
            cResponse res = new cResponse();

            long userID = Op_ns.UserAuth(model);

            res = new cResponse()
            {
                message_code = AppStaticInt.msg0001WrongUserNamePass_i,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };


            // return null if user not found
            if (userID == 0) return res;

            // authentication successful so generate jwt and refresh tokens
            string jwtToken = generateJwtToken(userID);
            //token bilgisinin DB ye kaydı
            if (savetoken(userID, jwtToken) == 0)
                return res;
            //Ram deki statik liste kullanıcı bilgilerinin kaydedilmesi
            savestaticList(userID);

            return new cResponse()
            {
                message_code = AppStaticInt.msg0005CorrectUsernamePass_i,
                message = AppStaticStr.msg0005CorrectUsernamePass,
                token = jwtToken,
                data = string.Empty
            };
        }

        private string generateJwtToken(long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppStaticStr.sec_JWTClaim);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private long savetoken(long userID, string token)
        {
            DateTime tar = DateTime.Now;
            long result = 0;
            allofusers e_allogusers = new allofusers()
            {
                appdatabase_type = AppStaticStr.core_dbTypeMYSQL,
                appdatabase_connstr = AppStaticStr.core_dbConnStr
            };

            List<tokensofusers> l_tok = Op_tokenofUsers.GetAlltokensofusers(string.Format("{0}={1} "
                , info_tokenofUsers.tokensofusers_tokensofusers_users_id, userID), ALLOFUSERS: e_allogusers);

            if (l_tok != null)
            {
                for (int i = 0; i < l_tok.Count; i++)
                {
                    if (Op_tokenofUsers.Deletetokensofusers(l_tok[0].tokensofusers_id, ALLOFUSERS: e_allogusers) == false)
                        return result;
                }
            }
            tokensofusers e_result = Op_tokenofUsers.Savetokensofusers(new tokensofusers()
            {
                deletedtokensofusers_id = false,
                tokensofusers_active = true,
                tokensofusers_createtime = tar,
                tokensofusers_expiretime = tar.AddMinutes(120),
                tokensofusers_id = 0,
                tokensofusers_refreshtime = tar,
                tokensofusers_token = token,
                tokensofusers_use = true,
                tokensofusers_users_id = userID
            }
            , ALLOFUSERS: e_allogusers);

            if (e_result.tokensofusers_id == 0)
                return result;
            else
                result = e_result.tokensofusers_id;


            return result;
        }

        private void savestaticList(long userID)
        {
            AppStaticModel.l_allofusers.RemoveAll(x => x.users_id == userID);

            List<allofusers> l_tmp = Op_Core.l_allofusers(userID, AppStaticStr.core_dbTypeMYSQL, AppStaticStr.core_dbConnStr);

            if (l_tmp != null)
            {
                for (int i = 0; i < l_tmp.Count; i++)
                {
                    AppStaticModel.l_allofusers.Add(l_tmp[i]);
                }
            }
        }

        public cResponse SaveNewUser(cRequest model, string ipAddress)
        {
            cResponse res = new cResponse();

            long userID = Op_ns.NewUser(model);


            // return null if user not found
            if (userID == 0) return new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };

            if (userID == -1) return new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0020SaveUsernamePassKayitli,
                token = "",
                data = string.Empty
            };


            return new cResponse()
            {
                message_code = AppStaticInt.msg001Succes,
                message = AppStaticStr.msg0015SaveUsernamePass,
                token = "",
                data = string.Empty
            };
        }

        public cResponse userActivation (string ACTkey)
        {
            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0025ActivasyonHatasi,
                token = string.Empty,
                data = string.Empty
            };


            long userid = Op_ns.UserIdofActivation(actkey: ACTkey);
            if (userid == 0)
                return response;

            response.message_code = AppStaticInt.msg001Succes;
            response.message = AppStaticStr.msg0030ActivasyonYapildi;


            return response;
        }

    }
}
