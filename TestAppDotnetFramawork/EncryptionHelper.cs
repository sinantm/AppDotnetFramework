using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestAppDotnetFramawork
{
  public class EncryptionHelper
  {
    public static string EncryptKey(string key, string password)
    {
      using (Aes aesAlg = Aes.Create())
      {
        Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

        aesAlg.Key = keyDerivation.GetBytes(32); // 256-bit anahtar
        aesAlg.IV = keyDerivation.GetBytes(16); // 128-bit IV

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
          using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
              swEncrypt.Write(key);
            }
          }
          return Convert.ToBase64String(msEncrypt.ToArray());
        }
      }
    }
    //request.AddParameter("apiKey", Config.SAPAPIKey, ParameterType.QueryString);
    public static string DecryptKey(string encryptedKey, string password)
    {
      try
      {
        using (Aes aesAlg = Aes.Create())
        {
          Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

          aesAlg.Key = keyDerivation.GetBytes(32);
          aesAlg.IV = keyDerivation.GetBytes(16);

          ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

          using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedKey)))
          {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
              using (StreamReader srDecrypt = new StreamReader(csDecrypt))
              {
                return srDecrypt.ReadToEnd();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        // Hata durumunda string.Empty dönebilirsiniz.
        return ex.Message;
      }
    }
  }
}
