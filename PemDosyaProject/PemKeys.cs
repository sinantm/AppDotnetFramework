using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using System.Collections.Generic;
using Jose;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IdentityModel.Tokens.Jwt;

namespace PemDosyaProject
{
    public class PemKeys
    {
        public static string ReadPemPrivateKeySigned(string content)
        {
            string pemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pems", "private.pem");


            try
            {
                using (var reader = new StreamReader(pemPath))
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


        public static string CreateSignedJWS(string requestBody)
        {

            // 2. Request body'yi JSON olarak serialize etme
            string serializedRequestBody = JsonConvert.SerializeObject(requestBody);

            // 3. Serialize edilen request body'yi UTF8 encoding ile hashleme
            byte[] hashBytes;
            using (var sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(serializedRequestBody));
            }

            // 4. Hash sonrası Base64 string'e dönüştürme
            string base64Hash = Convert.ToBase64String(hashBytes);

            // 5. Private key'i formBase64String'e dönüştürme
            string pemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pems", "private.pem");
            string privateKey;
            using (var reader = new StreamReader(pemPath))
            {
                privateKey = reader.ReadToEnd();
            }

            // 6. RSA provider'ı oluşturma
            var rsaProvider = new RSACryptoServiceProvider();

            // Private anahtarı oku
            var keyParams = (RsaPrivateCrtKeyParameters)new PemReader(new StringReader(privateKey)).ReadObject();

            // BouncyCastle RSA parametrelerini .NET RSA parametrelerine dönüştür
            var rsaParams = DotNetUtilities.ToRSAParameters(keyParams);
            rsaProvider.ImportParameters(rsaParams);

            // 7. JWT oluşturma ve imzalama
            var header = new Dictionary<string, object>
            {
                { "alg", "RS256" },
                { "typ", "JWT" }
            };
            string jws = JWT.Encode(base64Hash, rsaProvider, JwsAlgorithm.RS256, header);

            // 8. İmzalanan bu değer request header'da "Signature" alanında gönderilecek
            return jws;
        }


        public static string CreateSignedJWS1(string requestBody)
        {
            // 2. Request body'yi JSON olarak serialize etme
            //string serializedRequestBody = JsonConvert.SerializeObject(requestBody);

            // 3. Serialize edilen request body'yi UTF8 encoding ile hashleme
            byte[] hashBytes;
            using (var sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(requestBody));
            }

            // 4. Hash sonrası Base64 string'e dönüştürme
            string base64Hash = Convert.ToBase64String(hashBytes);

            // 5. Private key'i okuma'e dönüştürme
            //string pemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pems", "private.pem");



            string pemPath = @"-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDesXlpXsEVy6u9
y8pHm8qtHQCYLuXoXZjLWLjwZ9im2vyq1zxkuBoBYM+HhrGXbgZarEWG19PD0pcI
1Fvvek3hZASm2zKWjOIlN/Aw+OWFXgjdSsxxdqd8HYOg0thnFKcwTmdrJXxdMb2r
cAefkrwSVcDppIdgIZs07Ym1Bhr221zsQN44RXKgfId103YAthetyl7KIV6HbJjg
2Ae3e0bDfxWBir5tdNTJz412N6NmsjnIfGgmIJfA4rExcToz3PfZuj7nfgmn/zmB
uxbOfTKbdxqKyjGgV8N60mvJB8TBYQhYk1IOmx5ut48yLq5h6anFDarx/v/VynZk
83lofxoTAgMBAAECggEAB94md2cj5qypK4kuh8tt22tRwOoeHnVCYLX2Ur7qyugR
wKUnzRThxAqzAS/sf3iPF8UC79U99hic5pU+s2vcfa6oqSEQ3Q6GU3Nx60kEd304
vBjGJOnOjr9zDfAb8JqsezPcOBhPhakZ8UAUBN9DbuuK+drPZHhdxbWDTtshgeTf
ktO1a5fzV7uRpQxd/b9MmA7Wq0PnUAcg89iksxNkuINqG2Y+g7FkQoGSOR5CCUk2
vBj12nRXrbpTjNlWAlKtgA6+uI6wPracoYL83MCdlpQVYGRNuxoNN6PSseQP3CT8
vj/uxNs5XEzvKPHn8foCMf500XAYYoovFyEVQSyQgQKBgQD1boXzJ1ltYod+/DXf
mX15Kvx0AyU0Pd8p7TAzEXhSCran3AqAq0W3iVUoeOU5y6hm+PjLjIc+o/nwJpQ2
j+O8zjlxU+dFO2M4Xjdfo57q0of2XNBNqukEwe1YDWVdmKv+lItj6gU7lGzM4Sen
qfQxPqKRRcYikc8dnnsLdkYARQKBgQDoSEwTUyvSezwFHxxd8L6tnlmObh/EAjTf
9yS83HYXCcNWRnD+PUQ4npn3ZKEJf4Mfzol2EAF7wjc1ANinoHxovJDb7pmVtOLG
Hb6GbBhFudbSkDkGJBUfUzBBGGRLVmsFXUp4m0R02RczpA1/l6Xo626krXKpKu6H
40K/k4GydwKBgC2H6XWocSCnzLc0FtJ6aRqXbOogw0Aj0ki25eAzd3zQ2/3cBl7E
Z1SbN58gfnXwYDdqLM2mLljilrWEkq5klz0pjOKHTDo9wDRu1hr6AbtSf0KnCUW/
VSc6yssxAQMSFaZO7GUGvx3EdUK06mRiVRjlo8cLiKxVHHVEN1NzB515AoGBAIZL
FkCt+04LlZ3YaMwfHf6+6EQigxcNt4gtGP5f755ONLMysq0qusCJYbbYZQpawHKp
NGwfwNvOY6CvRpNTg9oB+zZMcltNYzbrh3WsFTRqxzqhy3YzckrUC7f25DVyVxmY
4C9uhVuRD4r5tBwqju9k/mkTJpGLkrDZwYOFQpztAoGAXUFctwMcN8mftyVprpy3
1u9n92eaUOvZlbGj1ZyAobe4SyY3OnDVE0B2c7Do9wM7ct2DeyuSKpJLjLh+dwlD
pihfsVufXWkhc+jeWVcSEH//S2Xs0SpnnKeFSE7hefACccIocERS7xYYLTZ+7Mq9
LP0kad9Ok5agIGO8oV+YkXY=
-----END PRIVATE KEY-----";




            string privateKey = pemPath;

            //dosya yolu ile dosya okuma
            //using (var reader = new StreamReader(pemPath))
            //{
            //    privateKey = reader.ReadToEnd();
            //}



            // 6. RSA provider'ı oluşturma
            var rsaProvider = new RSACryptoServiceProvider();


            var keyParams = (RsaPrivateCrtKeyParameters)new PemReader(new StringReader(privateKey)).ReadObject();

            if (keyParams == null)
            {
                throw new InvalidCastException("Signature (İmza) PEM dosyası geçersiz! RSA özel anahtar içermiyor.");
            }

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(keyParams);


            rsaProvider.ImportParameters(rsaParams);

            //using (var reader = new StreamReader(pemPath))
            //{
            //    PemReader pemReader = new PemReader(reader);
            //    object pemObject = pemReader.ReadObject();

            //    var keyParams = (RsaPrivateCrtKeyParameters)new PemReader(new StringReader(privateKey)).ReadObject();

            //    if (keyParams == null)
            //    {
            //        throw new InvalidCastException("Signature (İmza) PEM dosyası geçersiz! RSA özel anahtar içermiyor.");
            //    }

            //    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(keyParams);


            //    rsaProvider.ImportParameters(rsaParams);
            //}


            // 7. JWT oluşturma ve imzalama

            string jws = JWT.Encode(base64Hash, rsaProvider, JwsAlgorithm.RS256);


            return jws;
        }


    }
}
