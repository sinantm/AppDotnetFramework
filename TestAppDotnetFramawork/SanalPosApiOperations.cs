using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestAppDotnetFramawork
{
    public class SanalPosApiOperations
    {
        public async static Task<string> GetApiRequestToken()
        {
            //string StoreCode = "1001";
            //string CustomerCode = "500001";
            //string UserName = "api500001";
            //string Password = "C6D2F5A08B41C6A233BE7501DC7ED54EB4C118D1";

            string StoreCode = "1001";
            string CustomerCode = "5232017";
            string UserName = "aktasins";
            string PasswordHash = "47EDA7C142D8E34EFD2F90B693EF8EF4085C30D4";
            string TimeTick = DateTime.Now.Ticks.ToString();

            string data = string.Format("{0}{1}{2}{3}{4}", StoreCode, CustomerCode, UserName, TimeTick, PasswordHash);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            string AccountPasswordHash = Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data)));

            //header'a eklenecek Api-Token
            string apiToken = string.Format("{0}:{1}:{2}:{3}:{4}", StoreCode, CustomerCode, UserName, TimeTick, AccountPasswordHash);

            var stringContent = new StringContent(string.Empty);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Api-Token", apiToken);
                client.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/json");
                client.BaseAddress = new Uri("https://sanalpos.emas.com.tr");

                var responseTask = await client.PostAsync("/api/Payer/GetRates", stringContent);

                var result = responseTask.RequestMessage.Content;
            }

            return apiToken;
        }
    }
}
