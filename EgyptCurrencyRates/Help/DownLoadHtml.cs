using EgyptCurrencyRates.Models;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace EgyptCurrencyRates.Help
{
    static class DownLoadHtml
    {
        public static HtmlDocument DownLoadPageSource(string Url)
        {
            try
            {
                //Back: ;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
                //myWebRequest.UserAgent = RandomUserAgent();
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

                myWebRequest.Method = "GET";
                //IWebProxy proxy = new WebProxy(Proxy().Item1, Proxy().Item2);
                //IWebProxy proxy = new WebProxy("140.246.19.86", 8080);
                //myWebRequest.Proxy = proxy;
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                StreamReader myWebSource = new StreamReader(myWebResponse.GetResponseStream());
                string myPageSource = string.Empty;
                myPageSource = myWebSource.ReadToEnd();
                myWebResponse.Close();

                if (string.IsNullOrEmpty(myPageSource))
                {
                    //Thread.Sleep(60000);
                    //goto Back;
                    return new HtmlDocument();
                }

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(myPageSource);
                return htmlDocument;
            }
            catch (Exception ex)
            {
                //Thread.Sleep(60000);
                //DownLoadPageSource(Url);
                //ex.Message
                using (var db = new EgyptCurrencyRatesContext())
                {
                    db.Errors.Add(new Error { Message = ex.Message, Source = "DownLoadPage : " + Url });
                    db.SaveChanges();
                }
                return new HtmlDocument();
            }
        }
        public static string RandomUserAgent()
        {
            string[] UserAgents = new string[]{
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)",
           "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)",
           "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; en) Opera 8.0",
           "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)",
           "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/125.2 (KHTML, like Gecko) Safari/125.8",
           "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.29 Safari/525.13"};
            Random random = new Random();
            return UserAgents[random.Next(0, 5)].ToString();
        }
    }
}