using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppDotnetFramawork.Banks
{
  public class Banks
  {
    public static string ProcessForm(System.Web.HttpRequestBase Request)
    {
      List<KeyValuePair<String, String>> postParams = new List<KeyValuePair<String, String>>();
      foreach (string key in Request.Form.AllKeys)
      {
        //Response.Write("<input type=\"hidden\" name=\"" + key + "\" value=\"" + Request.Form[key] + "\" /><br />");
        KeyValuePair<String, String> newKeyValuePair = new KeyValuePair<String, String>(key, Request.Form[key]);
        postParams.Add(newKeyValuePair);

      }

      postParams.Sort(
          delegate (KeyValuePair<string, string> firstPair, KeyValuePair<string, string> nextPair)
          {
            return firstPair.Key.CompareTo(nextPair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false)));
          }
      );

      String hashVal = "";
      String storeKey = "TEST1234";
      storeKey = storeKey.Replace("\\", "\\\\").Replace("|", "\\|");

      foreach (KeyValuePair<String, String> pair in postParams)
      {
        String escapedValue = pair.Value.Replace("\\", "\\\\").Replace("|", "\\|");
        String lowerValue = pair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false));
        if (!"encoding".Equals(lowerValue) && !"hash".Equals(lowerValue))
        {
          hashVal += escapedValue + "|";
        }
      }
      hashVal += storeKey;

      System.Security.Cryptography.SHA512 sha = new System.Security.Cryptography.SHA512CryptoServiceProvider();
      byte[] hashbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(hashVal);
      byte[] inputbytes = sha.ComputeHash(hashbytes);
      String hash = System.Convert.ToBase64String(inputbytes);
      // add hash to hidden variable
      //Response.Write("<input type=\"hidden\" name=\"hash\" value=\"" + hash + "\" /><br />");




      return "";
    }
  }
}
