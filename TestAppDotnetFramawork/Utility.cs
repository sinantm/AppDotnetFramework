
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

namespace TestAppDotnetFramawork
{
    public class Utility
    {

        private static string ReadPemPrivateKeySigned(string pemFilePath, string content)
        {
            try
            {
                using (var reader = new StreamReader(pemFilePath))
                {
                    PemReader pemReader = new PemReader(reader);
                    object pemObject = pemReader.ReadObject();

                    var rsaPrivateCrtKeyParameters = pemObject as RsaPrivateCrtKeyParameters;

                    if (rsaPrivateCrtKeyParameters == null)
                    {
                        throw new InvalidCastException("Signature (İmza) PEM dosyası geçersiz! RSA özel anahtar içermiyor.");
                    }

                    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(rsaPrivateCrtKeyParameters);

                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(rsaParams);

                        byte[] dataBytes = Encoding.UTF8.GetBytes(content);
                        byte[] signedBytes = rsa.SignData(dataBytes, CryptoConfig.MapNameToOID("SHA256"));

                        return Convert.ToBase64String(signedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hata: Sistem yöneticinize danışın", ex);
            }
        }

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

                string signedData = PemDosyaProject.PemKeys.ReadPemPrivateKeySigned(Content);


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
    }
}
