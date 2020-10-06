using micro_services_share.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace micro_services_share
{
    public class AppClassMVCMethod
    {
        #region Comstructure
        HttpClient client;
        public AppClassMVCMethod(string token = "")
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            if (token.Length > 1)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #endregion Comstructure

        public cResponse post(string urladdr,cRequest model)
        {
            cResponse response = new cResponse()
            {
                token = "",
                data = "",
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata
            };

            try
            {
                var fromBody = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var downloadTask = client.PostAsync(urladdr, fromBody);
                downloadTask.Wait();
                var downloadResult = downloadTask.Result;
                var stringTask = downloadResult.Content.ReadAsStringAsync();
                stringTask.Wait();

                response = JsonConvert.DeserializeObject<cResponse>(stringTask.Result);

            }
            catch (Exception)
            {


            }

            return response;
        }
    }
}
