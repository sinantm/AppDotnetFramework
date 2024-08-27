using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
    }
}
