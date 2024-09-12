
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestAppDotnetFramawork.Models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Web;
using Newtonsoft.Json;
using Jose;

namespace TestAppDotnetFramawork
{
    public class Utility
    {
        public static ApiCallResponse ApiRequestPostV4(string SubscriptionKey, string url, string Content, List<RequestHeaderItem> ExtraHeaders = null)
        {
            ApiCallResponse resp = new ApiCallResponse();
            try
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                                             (se, cert, chain, sslerror) =>
                                             {
                                                 return true;
                                             };

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
                http.Accept = "application/json; charset=utf-8";
                http.ContentType = "application/json; charset=utf-8";

                http.Method = "POST";


                string pemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pems", "private.pem");

                //string signedData = PemDosyaProject.PemKeys.SignData(Content, pemPath);
                string signedData = PemDosyaProject.PemKeys.CreateSignedJWS1(Content);

                http.Headers.Add("Signature", signedData);

                http.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                if (ExtraHeaders != null)
                {
                    foreach (var item in ExtraHeaders)
                    {
                        http.Headers.Add(item.Key, item.Value);
                    }
                }

                string parsedContent = Content;
                Encoding encoding = Encoding.UTF8;
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string Result = sr.ReadToEnd();

                resp.StatusCode = response.StatusCode.ToString();
                resp.Content = Result;
            }
            catch (WebException e)
            {
                try
                {
                    if (e.Response != null)
                    {
                        using (WebResponse response = e.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            resp.StatusCode = httpResponse.StatusCode.ToString();
                            using (Stream data = response.GetResponseStream())
                            using (var reader = new StreamReader(data))
                            {
                                string text = reader.ReadToEnd();
                                resp.Content = text;
                            }
                        }
                    }
                    else
                    {
                        resp.StatusCode = e.Status.ToString();
                        resp.Content = e.Message;
                    }
                }
                catch (Exception ex)
                {
                    resp.StatusCode = "500";
                    resp.Content = ex.Message;

                }
            }
            return resp;
        }


        public static ApiCallResponse ApiRequestPostV40()
        {
            string SupScriptionKey = "1rA1DN5blbS8bafWusNUWq9AuAYK2FDfF8A2tjFfMgjJorTz4u";

            //Request body'e RequestDate eklendi
            var _requestbody = new
            {
                Header = new
                {
                    AppKey = SupScriptionKey,
                    Channel = "STELLANTISOTOMOTIV_INTERAPI",
                    TellerName = "",
                    CallerId = "",
                    ChannelSessionId = "",
                    ChannelRequestId = "",
                    RequestDate = DateTime.Now
                },
                Parameters = new[]
                {
                  new
                  {
                      AccountBranchCode = 1170,
                      CustomerNo = 19523921,
                      AccountSuffix = 351,
                      AssociationCode = "",
                      QueryDate = "2024-08-26T00:00:00",
                      EndDate = "2024-08-27T23:59:59"
                  }
                }
            };



            // Request body'yi JSON olarak serialize etme
            var requestBody = JsonConvert.SerializeObject(_requestbody);

            // Serialize edilen request body'yi UTF8 encoding ile hashleme
            byte[] hashBytes;
            using (var sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
            }

            // Hash sonrası Base64 string'e dönüştürme
            string base64Hash = Convert.ToBase64String(hashBytes);

            // Private key'i okuma'e dönüştürme
            string pemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pems", "private.pem");


            string privateKey = string.Empty;
            using (var reader = new StreamReader(pemPath))
            {
                privateKey = reader.ReadToEnd();
            }


            // RSA provider'ı oluşturma
            var rsaProvider = new RSACryptoServiceProvider();

            using (var reader = new StreamReader(pemPath))
            {
                PemReader pemReader = new PemReader(reader);
                object pemObject = pemReader.ReadObject();

                var keyParams = (RsaPrivateCrtKeyParameters)new PemReader(new StringReader(privateKey)).ReadObject();

                if (keyParams == null)
                {
                    throw new InvalidCastException("Signature (İmza) PEM dosyası geçersiz! RSA özel anahtar içermiyor.");
                }

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(keyParams);


                rsaProvider.ImportParameters(rsaParams);
            }


            // Oluşan değer provider JwsAlgorithm.RS256 ile jwt encode edilir

            string Signature = JWT.Encode(base64Hash, rsaProvider, JwsAlgorithm.RS256);



            //ATILAN REQUEST
            ApiCallResponse resp = new ApiCallResponse();
            try
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                var queryString = HttpUtility.ParseQueryString(string.Empty);

                var http = (HttpWebRequest)WebRequest.Create(new Uri("https://apigw.denizbank.com/api/v2/GetCorporateAccountTransactionList?{queryString}"));
                http.Accept = "application/json; charset=utf-8";
                http.ContentType = "application/json; charset=utf-8";

                http.Method = "POST";

                //Header'a Signature eklenmesi
                http.Headers.Add("Signature", Signature);

                http.Headers.Add("Ocp-Apim-Subscription-Key", SupScriptionKey);

                string parsedContent = requestBody;
                Encoding encoding = Encoding.UTF8;
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                string Result = sr.ReadToEnd();

                resp.StatusCode = response.StatusCode.ToString();
                resp.Content = Result;
            }
            catch (WebException e)
            {
                try
                {
                    if (e.Response != null)
                    {
                        using (WebResponse response = e.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            resp.StatusCode = httpResponse.StatusCode.ToString();
                            using (Stream data = response.GetResponseStream())
                            using (var reader = new StreamReader(data))
                            {
                                string text = reader.ReadToEnd();
                                resp.Content = text;
                            }
                        }
                    }
                    else
                    {
                        resp.StatusCode = e.Status.ToString();
                        resp.Content = e.Message;
                    }
                }
                catch (Exception ex)
                {
                    resp.StatusCode = "500";
                    resp.Content = ex.Message;

                }
            }
            return resp;
        }
    }
}
