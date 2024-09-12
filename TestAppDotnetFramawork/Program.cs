using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestAppDotnetFramawork.ResultNew;

namespace TestAppDotnetFramawork
{
  class Program
  {

    public static void Main()
    {
      #region SİFRELEME ISLEMI
      //ŞİFRELEME İŞLEMİ
      //string password = "AselsanNet#22xL!Qz66uHd";
      //string originalKey = "69971BC1-A8F8-4111-9FC7-1BCA2E7DB939";

      //string encryptedKey = EncryptionHelper.EncryptKey(originalKey, password);
      //Console.WriteLine($"Şifrelenmiş Anahtar: {encryptedKey}");

      //string decryptedKey = EncryptionHelper.DecryptKey(encryptedKey, password);
      //Console.WriteLine($"Çözülmüş Anahtar: {decryptedKey}");
      #endregion

      #region HASH ISLEMI


      #region Birinci Adım

      //StringBuilder HashDataReq = new StringBuilder();

      //HashDataReq.Append("15000,00" + "|"); //amount
      //HashDataReq.Append("2" + "|"); // cardType
      //HashDataReq.Append("500432073" + "|"); //clientid 
      //HashDataReq.Append("949" + "|"); //currency
      //HashDataReq.Append("370" + "|"); //cv2
      //HashDataReq.Append("05" + "|"); //Ecom_Payment_Card_ExpDate_Month
      //HashDataReq.Append("26" + "|"); //Ecom_Payment_Card_ExpDate_Year
      //HashDataReq.Append("https://sanalpos.aktulkagit.com.tr/Payment/ResponseHalkbank3d" + "|"); //failUrl
      //HashDataReq.Append("ver3" + "|"); //hashAlgorithm
      //HashDataReq.Append("tr" + "|"); //lang
      //HashDataReq.Append("20240401155037949100" + "|"); //oid
      //HashDataReq.Append("https://sanalpos.aktulkagit.com.tr/Payment/ResponseHalkbank3d" + "|"); //okUrl
      //HashDataReq.Append("5528 79** **** 1729" + "|"); //pan
      //HashDataReq.Append("sDxW5/R6Y0y1MXcMadWH" + "|"); //Rnd
      //HashDataReq.Append("3d"); //storetType
      //HashDataReq.Append("|" + "Auth");
      //HashDataReq.Append("|" + "1Sakarya*");

      //SHA512 sha1 = new SHA512CryptoServiceProvider();
      //byte[] hashbytes1 = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(HashDataReq.ToString());
      //byte[] inputbytes1 = sha1.ComputeHash(hashbytes1);
      //string Hash1 = Convert.ToBase64String(inputbytes1);

      //Console.WriteLine("Hash: " + Hash1);

      #endregion

      #region IkinciAdım
      //List<KeyValuePair<String, String>> postParams = new List<KeyValuePair<String, String>>();


      //postParams.Add(new KeyValuePair<string, string>("amount", "15000,00"));
      //postParams.Add(new KeyValuePair<string, string>("callbackCall", "true"));
      //postParams.Add(new KeyValuePair<string, string>("cavv", "ABIBBnQAAP29AVAAAAAAAAAAAAA="));
      //postParams.Add(new KeyValuePair<string, string>("clientid", "500432073"));
      //postParams.Add(new KeyValuePair<string, string>("clientIp", "78.182.140.197"));
      //postParams.Add(new KeyValuePair<string, string>("currency", "949"));
      //postParams.Add(new KeyValuePair<string, string>("dsId", "2"));
      //postParams.Add(new KeyValuePair<string, string>("eci", "02"));
      //postParams.Add(new KeyValuePair<string, string>("Ecom_Payment_Card_ExpDate_Month", "05"));
      //postParams.Add(new KeyValuePair<string, string>("Ecom_Payment_Card_ExpDate_Year", "26"));
      //postParams.Add(new KeyValuePair<string, string>("encoding", "UTF-8"));
      //postParams.Add(new KeyValuePair<string, string>("failUrl", "https://sanalpos.aktulkagit.com.tr/Payment/ResponseHalkbank3d"));
      //postParams.Add(new KeyValuePair<string, string>("HASH", "QwAvbV6YbA/V90mtfhCryB7Yb+ONvYIaDzDEMy8QkkhvO9wpb/KFXWtXtJx29I4BMjfkfL1eKbGFwxZdxwHzXQ=="));
      //postParams.Add(new KeyValuePair<string, string>("hashAlgorithm", "ver3"));
      //postParams.Add(new KeyValuePair<string, string>("lang", "tr"));
      //postParams.Add(new KeyValuePair<string, string>("maskedCreditCard", "5528 79** **** 1729"));
      //postParams.Add(new KeyValuePair<string, string>("MaskedPan", "552879***1729"));
      //postParams.Add(new KeyValuePair<string, string>("md", "552879:847A9A3BAFA2885D57FD162CE440492E3358A52EFBD2140F00AAAAD5CEB2EA98:4019:##500432073"));
      //postParams.Add(new KeyValuePair<string, string>("mdErrorMsg", "Success"));
      //postParams.Add(new KeyValuePair<string, string>("mdStatus", "1"));
      //postParams.Add(new KeyValuePair<string, string>("merchantName", "AKTÜL KAĞIT ÜRETİM PAZ. A.Ş."));
      //postParams.Add(new KeyValuePair<string, string>("oid", "20240401155037949100"));
      //postParams.Add(new KeyValuePair<string, string>("okUrl", "https://sanalpos.aktulkagit.com.tr/Payment/ResponseHalkbank3d"));
      //postParams.Add(new KeyValuePair<string, string>("rnd", "sDxW5/R6Y0y1MXcMadWH"));
      //postParams.Add(new KeyValuePair<string, string>("storetype", "3d"));
      //postParams.Add(new KeyValuePair<string, string>("TranType", "Auth"));
      //postParams.Add(new KeyValuePair<string, string>("xid", "lhbhU5AGF6LpfiX8Xeuo28dwXeQ="));



      //postParams = postParams.OrderBy(pair => pair.Key.ToLower(), StringComparer.InvariantCultureIgnoreCase).ToList();


      //String hashVal = "";
      //String storeKey = "1Sakarya*";

      //foreach (KeyValuePair<String, String> pair in postParams)
      //{
      //  String escapedValue = pair.Value.Replace("\\", "\\\\").Replace("|", "\\|");
      //  String lowerValue = pair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false));


      //  if (!"encoding".Equals(lowerValue) && !"hash".Equals(lowerValue))
      //  {
      //    hashVal += escapedValue + "|";
      //  }
      //}
      //hashVal += storeKey;


      //String hashparam = postParams.Where(x => x.Key == "HASH").FirstOrDefault().Value;

      //System.Security.Cryptography.SHA512 sha = new System.Security.Cryptography.SHA512CryptoServiceProvider();
      //byte[] hashbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(hashVal);
      //byte[] inputbytes = sha.ComputeHash(hashbytes);
      //String hash = Convert.ToBase64String(inputbytes);


      //if (!hash.Equals(hashparam))
      //{
      //  Console.WriteLine("HASH hatalı!!!");
      //}
      //else
      //{
      //  Console.WriteLine("HASH Başarılı!!!");
      //}
      #endregion

      #endregion

      #region MAIL ISLEMI
      //MAİL İŞLEMİ
      ////string host = "smtp-mail.outlook.com";
      //string host = "smtp.office365.com";
      ////string host = "31.210.39.219";
      //int port = 587;

      //string address = "siparis@kombiklimashop.com";
      //string password = "KksSip21*";

      ////string address = "ilkcalistir@kombiklimashop.com";
      //string password = "Kksinfo17*";

      ////eski başarılı olan
      ////string address = "kksmailtest@innthebox.com";
      ////string password = "Roq82094";



      //SmtpClient smtpClient = new SmtpClient
      //{
      //  Host = host,
      //  Port = port,
      //  EnableSsl = true,
      //  Credentials = new NetworkCredential(address, password)
      //};

      //MailAddress from = new MailAddress(address);
      //MailAddress to = new MailAddress("ademozdemir@outlook.com");

      //MailMessage message = new MailMessage
      //{
      //  From = from
      //};
      //string htmlCode = @"
      //                   <html lang=""tr"">
      //                       <head>
      //                           <link rel=""shortcut icon"" type=""image/x-icon"" href=""https://kombiklimashop.com/favicon_.ico"" />
      //                           <title>Sipariş Formu</title>
      //                       </head>
      //                       <body>
      //                           <!-- START main table -->
      //                           <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
      //                               <tbody>
      //                                   <tr>
      //                                       <td bgcolor=""#FFFFFF"" align=""center"">
      //                                           <div align=""center"">
      //                                               <!-- START main centred table -->
      //                                               <table width=""630"" border=""0"" cellpadding=""0"" cellspacing=""0"">
      //                                                   <tbody>
      //                                                       <tr>
      //                                                           <td>
      //                                                               <!-- START main content table -->
      //                                                               <table width=""630"" border=""0"" cellspacing=""0"" cellpadding=""0"">
      //                                                                   <!-- START module / header with logo only -->
      //                                                                   <tbody>
      //                                                                       <tr>
      //                                                                           <td bgcolor=""#FFFFFF"" valign=""top"" style=""border-right: none; border-bottom: none; border-left: none; padding-bottom: 0px;"" align=""center"">
      //                                                                               <!-- divider goes here -->
      //                                                                               <img src=""https://kombiklimashop.com/images/mail/kombi_klima_shop.png"" alt="""" border=""no"" style=""margin: 0px; padding: 0px; display: block;"" height=""30"" />
      //                                                                           </td>
      //                                                                       </tr>
      //                                                                       <tr>
      //                                                                           <td bgcolor=""#FFFFFF"" valign=""top"" style=""border-top: none; border-right: none; border-bottom: none; border-left: none; padding-bottom: 10px;"">
      //                                                                               <!-- divider goes here -->
      //                                                                               <img src=""https://kombiklimashop.com/images/mail/splitted-header.jpg"" alt="""" border=""no"" style=""margin: 0px; padding: 0px; display: block;"" />
      //                                                                           </td>
      //                                                                       </tr>
      //                                                                       <!-- END module -->
      //                                                                       <!-- START module / three column content texts -->
      //                                                                       <tr>
      //                                                                           <td bgcolor=""#FFFFFF"" valign=""top"" style=""border-top: none; border-right: none; border-bottom: none; border-left: none; padding-bottom: 0px;"">
      //                                                                               <table width=""630"" border=""0"" cellspacing=""0"" cellpadding=""0"">
      //                                                                                   <tbody>
      //                                                                                       <tr>
      //                                                                                           <!-- start left table column -->

      //                                                                                           <td align=""center"" valign=""top"" style=""font-family: Arial, Helvetica, sans-serif; font-size: 18px; line-height: 22px; color: #54a9d0; padding-bottom: 10px;"">
      //                                                                                               <!-- title goes here -->
      //                                                                                               SİPARİŞ
      //                                                                                           </td>
      //                                                                                       </tr>
      //                                                                                   </tbody>
      //                                                                               </table>
      //                                                                           </td>
      //                                                                       </tr>
      //                                                                   </tbody>
      //                                                               </table>
      //                                                           </td>
      //                                                       </tr>
      //                                                   </tbody>
      //                                               </table>
      //                                           </div>
      //                                       </td>
      //                                   </tr>
      //                               </tbody>
      //                           </table>
      //                       </body>
      //                   </html>";


      //try
      //{
      //  message.To.Add(to);
      //  message.Subject = "Test Subject";
      //  message.Body = htmlCode;
      //  message.IsBodyHtml = true;
      //  message.SubjectEncoding = System.Text.Encoding.UTF8;

      //  smtpClient.Send(message);

      //  Console.WriteLine("Mail gönderimi başarılı");
      //}
      //catch (Exception ex)
      //{
      //  Console.WriteLine(ex.Message);
      //  Console.ReadLine();
      //}
      #endregion

      #region SANALPOS_API_OPERATION
      //var response = SanalPosApiOperations.GetApiRequestToken().Result;





      //Console.WriteLine("Token: " + response);

      #endregion

      #region DENİZBANK DIJITAL İMZA BANK POST CANLI
      //MLPCARE
      //{ "Header":{ "AppKey":"kYMPvzHs4yU3N6VD9ZHewAhxOlkH2dK7EymgC7qrrLUtpHQJ2w","Channel":"NETBT","TellerName":"","CallerId":"","ChannelSessionId":"","ChannelRequestId":""},"Parameters":[{ "AccountBranchCode":3390,"CustomerNo":2459657,"AccountSuffix":447,"AssociationCode":" ","QueryDate":"2024-08-25T00:00:00","EndDate":"2024-08-26T23:59:59"}]}

      //STELLANTIS
      //{ "Header":{ "AppKey":"1rA1DN5blbS8bafWusNUWq9AuAYK2FDfF8A2tjFfMgjJorTz4u","Channel":"STELLANTISOTOMOTIV_INTERAPI","TellerName":"","CallerId":"","ChannelSessionId":"","ChannelRequestId":""},"Parameters":[{ "AccountBranchCode":1170,"CustomerNo":19523921,"AccountSuffix":351,"AssociationCode":" ","QueryDate":"2024-08-26T00:00:00","EndDate":"2024-08-27T23:59:59"}]}


      //string SupScriptionKey = "1rA1DN5blbS8bafWusNUWq9AuAYK2FDfF8A2tjFfMgjJorTz4u";

      //DateTime requestdate = DateTime.Now;

      //var _body = new
      //{
      //    Header = new
      //    {
      //        AppKey = SupScriptionKey,
      //        Channel = "STELLANTISOTOMOTIV_INTERAPI",
      //        TellerName = "",
      //        CallerId = "",
      //        ChannelSessionId = "",
      //        ChannelRequestId = "",
      //        RequestDate = requestdate,
      //        AssociationCode = " ",
      //    },
      //    Parameters = new[]
      //    {
      //      new
      //      {
      //          AccountBranchCode = 1170,
      //          CustomerNo = 19523921,
      //          AccountSuffix = 351,
      //          AssociationCode = "",
      //          QueryDate = "2024-08-26T00:00:00",
      //          EndDate = "2024-08-27T23:59:59"
      //      }
      //    }
      //};


      //var queryString = HttpUtility.ParseQueryString(string.Empty);
      //var uri = $"https://apigw.denizbank.com/api/v2/GetCorporateAccountTransactionList?{queryString}";
      //var body = JsonConvert.SerializeObject(_body);

      //var response = Utility.ApiRequestPostV4(SupScriptionKey, uri, body);

      //Console.WriteLine("Status Code: " + response.StatusCode);
      //Console.WriteLine("Yanıt: " + response.Content);
      #endregion

      #region SAPMA SERVİCE TEST
      try
      {
        var client = new YNE_WS_MTBKT_RESULT_NEW();

        client.Url = "http://S4ERPAPPQA.sampa.com:8000/sap/bc/srt/rfc/sap/yne_ws_mtbkt_result_new/400/yne_ws_mtbkt_result_new/yne_ws_mtbkt_result_new";

        var credentials = new System.Net.NetworkCredential("sarslan", "Netbt2023*");
        client.Credentials = credentials;

        YneMtbktResultNew yneMtbktResultNew = new YneMtbktResultNew();

        yneMtbktResultNew.IAnswer = "Y";
        yneMtbktResultNew.IFile_1 = "";
        yneMtbktResultNew.IFile_2 = "";
        yneMtbktResultNew.IIp = "168.198.1.1";
        yneMtbktResultNew.INoData = "";
        yneMtbktResultNew.IRandid = "1BQQ8CM6EU1XJASH16TL8E91U4II5DOHPA3OE8RZPT2HHPZA4S";
        yneMtbktResultNew.IRpmail = "";
        yneMtbktResultNew.ISpras = "";
        yneMtbktResultNew.ITerminal = "dektop";
        yneMtbktResultNew.IText = "test";
        yneMtbktResultNew.IUnvan = "";
        yneMtbktResultNew.IUser = "sinan temel";

        var res = client.YneMtbktResultNew(yneMtbktResultNew);
      }
      catch (Exception ex)
      {

        throw;
      }
      #endregion



      Console.Read();
    }
  }
}
