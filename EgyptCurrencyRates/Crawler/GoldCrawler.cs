using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptCurrencyRates.Crawler
{
    public class GoldCrawler
    {
        public void Goldrate24()
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = DownLoadHtml.DownLoadPageSource("https://www.goldrate24.com/gold-prices/middle-east/egypt/");


            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();
            List<GoldPrice> priceList = new List<GoldPrice>();
            List<GoldPriceLog> priceLogList = new List<GoldPriceLog>();


            var Summary = htmlDocument.DocumentNode.Descendants("div")
                   .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Equals("summary"))
                   .Select(d => d.Descendants("tbody")).FirstOrDefault().FirstOrDefault().Descendants("tr").ToList();

            #region Summary                 
            var Gold_Gram_Carat_24 = Summary[2].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_22 = Summary[3].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_21 = Summary[4].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_18 = Summary[5].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_14 = Summary[6].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_12 = Summary[7].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_10 = Summary[8].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_09 = Summary[9].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gold_Gram_Carat_08 = Summary[10].Descendants("td").Select(t => t.InnerText.Trim()).ToList();

            priceList.Add(new GoldPrice { GoldUnit = 2, EgyptianPound = Gold_Gram_Carat_24[0].ToString(), UsaDollar = Gold_Gram_Carat_24[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 3, EgyptianPound = Gold_Gram_Carat_22[0].ToString(), UsaDollar = Gold_Gram_Carat_22[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 4, EgyptianPound = Gold_Gram_Carat_21[0].ToString(), UsaDollar = Gold_Gram_Carat_21[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 5, EgyptianPound = Gold_Gram_Carat_18[0].ToString(), UsaDollar = Gold_Gram_Carat_18[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 6, EgyptianPound = Gold_Gram_Carat_14[0].ToString(), UsaDollar = Gold_Gram_Carat_14[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 7, EgyptianPound = Gold_Gram_Carat_12[0].ToString(), UsaDollar = Gold_Gram_Carat_12[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 8, EgyptianPound = Gold_Gram_Carat_10[0].ToString(), UsaDollar = Gold_Gram_Carat_10[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 9, EgyptianPound = Gold_Gram_Carat_09[0].ToString(), UsaDollar = Gold_Gram_Carat_09[1].ToString(), Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 10, EgyptianPound = Gold_Gram_Carat_08[0].ToString(), UsaDollar = Gold_Gram_Carat_08[1].ToString(), Date = Analytics.DateTime() });
            #endregion


            #region Per_Gram

            var Gram = htmlDocument.DocumentNode.Descendants("div")
                 .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("gram"))
                  .Select(d => d.Descendants("tbody")).FirstOrDefault().FirstOrDefault().Descendants("tr").ToList();

            var Gram_24K = Gram[0].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_22K = Gram[1].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_21K = Gram[2].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_18K = Gram[3].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_14K = Gram[4].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_12K = Gram[5].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_10K = Gram[6].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_09K = Gram[7].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Gram_08K = Gram[8].Descendants("td").Select(t => t.InnerText.Trim()).ToList();


            priceList.Add(new GoldPrice { GoldUnit = 22, EgyptianPound = Gram_24K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 23, EgyptianPound = Gram_22K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 25, EgyptianPound = Gram_21K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 26, EgyptianPound = Gram_18K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 27, EgyptianPound = Gram_14K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 29, EgyptianPound = Gram_12K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 30, EgyptianPound = Gram_10K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 31, EgyptianPound = Gram_09K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 32, EgyptianPound = Gram_08K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });

            #endregion
            #region Per_Ounce

            var Ounce = htmlDocument.DocumentNode.Descendants("div")
                          .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("ounce"))
                      .Select(d => d.Descendants("tbody")).FirstOrDefault().FirstOrDefault().Descendants("tr").ToList();



            var Ounce_24K = Ounce[0].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_22K = Ounce[1].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_21K = Ounce[2].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_18K = Ounce[3].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_14K = Ounce[4].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_12K = Ounce[5].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_10K = Ounce[6].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_09K = Ounce[7].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Ounce_08K = Ounce[8].Descendants("td").Select(t => t.InnerText.Trim()).ToList();

            priceList.Add(new GoldPrice { GoldUnit = 11, EgyptianPound = Ounce_24K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 12, EgyptianPound = Ounce_22K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 13, EgyptianPound = Ounce_21K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 14, EgyptianPound = Ounce_18K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 15, EgyptianPound = Ounce_14K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 16, EgyptianPound = Ounce_12K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 19, EgyptianPound = Ounce_10K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 20, EgyptianPound = Ounce_09K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 21, EgyptianPound = Ounce_08K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });

            #endregion



            #region Per_Kilogram

            var Kilogram = htmlDocument.DocumentNode.Descendants("div")
                 .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("kilogram"))
                 .Select(d => d.Descendants("tbody")).FirstOrDefault().FirstOrDefault().Descendants("tr").ToList();

            var Kilogram_24K = Kilogram[0].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_22K = Kilogram[1].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_21K = Kilogram[2].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_18K = Kilogram[3].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_14K = Kilogram[4].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_12K = Kilogram[5].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_10K = Kilogram[6].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_09K = Kilogram[7].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Kilogram_08K = Kilogram[8].Descendants("td").Select(t => t.InnerText.Trim()).ToList();

            priceList.Add(new GoldPrice { GoldUnit = 33, EgyptianPound = Kilogram_24K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 34, EgyptianPound = Kilogram_22K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 38, EgyptianPound = Kilogram_21K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 39, EgyptianPound = Kilogram_18K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 40, EgyptianPound = Kilogram_14K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 41, EgyptianPound = Kilogram_12K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 42, EgyptianPound = Kilogram_10K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 43, EgyptianPound = Kilogram_09K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 44, EgyptianPound = Kilogram_08K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });

            #endregion

            #region Per_Tola

            var Tola = htmlDocument.DocumentNode.Descendants("div")
                      .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("tola"))
                  .Select(d => d.Descendants("tbody")).FirstOrDefault().FirstOrDefault().Descendants("tr").ToList();

            var Tola_24K = Tola[0].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_22K = Tola[1].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_21K = Tola[2].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_18K = Tola[3].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_14K = Tola[4].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_12K = Tola[5].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_10K = Tola[6].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_09K = Tola[7].Descendants("td").Select(t => t.InnerText.Trim()).ToList();
            var Tola_08K = Tola[8].Descendants("td").Select(t => t.InnerText.Trim()).ToList();

            priceList.Add(new GoldPrice { GoldUnit = 45, EgyptianPound = Tola_24K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 46, EgyptianPound = Tola_22K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 47, EgyptianPound = Tola_21K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 48, EgyptianPound = Tola_18K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 49, EgyptianPound = Tola_14K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 50, EgyptianPound = Tola_12K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 51, EgyptianPound = Tola_10K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 52, EgyptianPound = Tola_09K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            priceList.Add(new GoldPrice { GoldUnit = 53, EgyptianPound = Tola_08K[0].ToString(), UsaDollar = "00", Date = Analytics.DateTime() });
            #endregion


            if (priceList.Count > 0)
            {
                db.GoldPrices.RemoveRange(db.GoldPrices.ToList());
                db.GoldPrices.AddRange(priceList);
                db.SaveChanges();
            }
        }
    }
}
