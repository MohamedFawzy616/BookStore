using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;

namespace EgyptCurrencyRates.Crawler
{
    public class EgyptRatesCrawler
    {
        public void Start()
        {
            List<Rate> Rates;

            HtmlAgilityPack.HtmlDocument home_htmlDocument = DownLoadHtml.DownLoadPageSource("https://egrates.com");//egyptrates
            if (home_htmlDocument == null) { return; }

            var BanksTr = home_htmlDocument.DocumentNode.Descendants("table")
                .Where(t => t.Attributes.Contains("class") && t.Attributes["class"].Value.Equals("table tbl-curr table-striped  table-hover  no-footer"))
                .Select(t => t.Descendants("tbody")).FirstOrDefault().SelectMany(t => t.Descendants("tr"));



            List<string> Banks = new List<string>();
            foreach (var tr in BanksTr)
            {
                Banks.Add("https://egrates.com/" + tr.Descendants("td").FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["href"].Value.Trim());
            }
            try
            {
                foreach (var bank in Banks)
                {
                    HtmlAgilityPack.HtmlDocument bank_htmlDocument = DownLoadHtml.DownLoadPageSource(bank);
                    if (bank_htmlDocument == null) { return; }

                    var CurrencyTrs = bank_htmlDocument.DocumentNode.Descendants("table")
                        .Where(t => t.Attributes.Contains("class") && t.Attributes["class"].Value.Equals("table tbl-curr table-striped  table-hover  no-footer"))
                        .Select(t => t.Descendants("tbody")).FirstOrDefault().SelectMany(t => t.Descendants("tr"));

                    Rates = new List<Rate>();
                    foreach (var currecny in CurrencyTrs)
                    {
                        Rate rate = new Rate();

                        var tds = currecny.Descendants("td");

                        string currency = tds.ElementAt(1).Descendants("img").FirstOrDefault().Attributes["title"].Value.Trim();
                        int ratebankID = int.Parse(tds.ElementAt(0).Descendants("a").FirstOrDefault().Attributes["href"].Value.Replace("banks/", "").Trim());

                        rate.BankId = GetBankId(ratebankID);
                        rate.CurrencyId = GetCurrencyID(currency);


                        rate.BuyPrice = double.Parse(currecny.Descendants("td").ElementAt(2).Descendants("a").FirstOrDefault().InnerText.Trim());
                        rate.SalePrice = double.Parse(currecny.Descendants("td").ElementAt(3).Descendants("a").FirstOrDefault().InnerText.Trim());

                        rate.TransferBuy = 0;
                        rate.TransferSale = 0;
                        rate.Date = Analytics.DateTime();
                        Rates.Add(rate);

                        SaveRates(Rates);
                    }
                }
                //ClearRatesTable(BankIds);
            }
            catch (Exception)
            {
            }
        }

        public void GetBank(string bank)
        {
            try
            {
                List<Rate> Rates;

                HtmlAgilityPack.HtmlDocument bank_htmlDocument = DownLoadHtml.DownLoadPageSource(bank);
                if (bank_htmlDocument == null) { return; }

                var CurrencyTrs = bank_htmlDocument.DocumentNode.Descendants("table")
                    .Where(t => t.Attributes.Contains("class") && t.Attributes["class"].Value.Equals("table tbl-curr table-striped  table-hover  no-footer"))
                    .Select(t => t.Descendants("tbody")).FirstOrDefault().SelectMany(t => t.Descendants("tr"));

                Rates = new List<Rate>();
                foreach (var currecny in CurrencyTrs)
                {
                    Rate rate = new Rate();

                    var tds = currecny.Descendants("td");

                    string currency = tds.ElementAt(1).Descendants("img").FirstOrDefault().Attributes["title"].Value.Trim();
                    int ratebankID = int.Parse(tds.ElementAt(0).Descendants("a").FirstOrDefault().Attributes["href"].Value.Replace("banks/", "").Trim());

                    rate.BankId = GetBankId(ratebankID);
                    rate.CurrencyId = GetCurrencyID(currency);


                    rate.BuyPrice = double.Parse(currecny.Descendants("td").ElementAt(2).Descendants("a").FirstOrDefault().InnerText.Trim());
                    rate.SalePrice = double.Parse(currecny.Descendants("td").ElementAt(3).Descendants("a").FirstOrDefault().InnerText.Trim());

                    rate.TransferBuy = 0;
                    rate.TransferSale = 0;
                    rate.Date = Analytics.DateTime();
                    Rates.Add(rate);

                    SaveBankRates(Rates);
                }
            }
            catch (Exception)
            {
            }
        }


        private void SaveBankRates(List<Rate> Rates)
        {
            using (EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext())
            {
                foreach (var rate in Rates)
                {
                    Rate existRate = db.Rates.Where(r => r.CurrencyId == rate.CurrencyId && r.BankId == rate.BankId).FirstOrDefault();
                    if (existRate != null)
                    {
                        db.Remove(existRate);
                    }
                    db.Add(rate);
                    db.SaveChanges();
                }
            }
        }


        List<int> BankIds = new List<int>();
        private void SaveRates(List<Rate> Rates)
        {
            using (EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext())
            {
                foreach (var rate in Rates)
                {
                    try
                    {
                        Rate existRate = db.Rates.Where(r => r.CurrencyId == rate.CurrencyId && r.BankId == rate.BankId).FirstOrDefault();
                        if (existRate != null)
                        {
                            db.Rates.Remove(existRate);
                        }
                        db.Rates.Add(rate);
                        db.SaveChanges();
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void ClearRatesTable(List<int> BankIds)
        {
            using (var db = new EgyptCurrencyRatesContext())
            {
                List<int> obsoleteDankIds = db.Banks.Select(b => b.Id).ToList();
                foreach (var id in obsoleteDankIds.Except(BankIds).Except(BankIds))
                {
                    var rates = db.Rates.Where(r => r.BankId == id);
                    if (rates.Count() > 0)
                    {
                        db.Rates.RemoveRange(rates);
                        db.SaveChanges();
                    }
                }
            }
        }

        private int GetCurrencyID(string currency)
        {
            switch (currency)
            {
                case "دينار كويتي/KWD":
                    return (int)DollarTodayCurrency.الدينار_الكويتي;
                case "دينار بحريني/BHD":
                    return (int)DollarTodayCurrency.الدينار_البحريني;
                case "ريال عماني/OMR":
                    return (int)DollarTodayCurrency.الريال_العماني;
                case "درهم إماراتي/AED":
                    return (int)DollarTodayCurrency.الدرهم_الإماراتي;
                case "ريال سعودي/SAR":
                    return (int)DollarTodayCurrency.الريال_السعودي;
                case "ريال قطري/QAR":
                    return (int)DollarTodayCurrency.الريال_القطري;
                case "دينار إردني/JOD":
                    return (int)DollarTodayCurrency.الدينار_الإردني;
                case "جنيه إسترلينى/GBP":
                    return (int)DollarTodayCurrency.الإسترليني;
                case "كرونا دنماركي/DKK":
                    return (int)DollarTodayCurrency.الكرونة_الدنماركى;
                case "يورو/EUR":
                    return (int)DollarTodayCurrency.اليورو;
                case "فرنك سويسرى/CHF":
                    return (int)DollarTodayCurrency.الفرنك;
                case "دولار امريكي/USD":
                    return (int)DollarTodayCurrency.الدولار;
                case "دولار كندي/CAD":
                    return (int)DollarTodayCurrency.الدولار_الكندى;
                case "دولار إسترالي/AUD":
                    return (int)DollarTodayCurrency.الدولار_الاسترالي;
                case "كرونا سويدى/SEK":
                    return (int)DollarTodayCurrency.الكرونة_السويدية;
                case "كرونا نرويجي/NOK":
                    return (int)DollarTodayCurrency.الكرونة_النرويجية;
                case "ين ياباني/JPY":
                    return (int)DollarTodayCurrency.الين;
                case "اليوان الصينى​/CNY":
                    return (int)DollarTodayCurrency.اليوان_الصينى;
                case "البات التايلندي/THB":
                    return (int)DollarTodayCurrency.البات_التايلندي;
                case "ليرة لبنانى/LBP":
                    return (int)DollarTodayCurrency.ليرة_لبناني;
                default:
                    return 0;
            }
        }

        public int GetBankId(int rateBankId)
        {
            int bankID = 0;

            switch (rateBankId)
            {
                case (int)EgyptRatesBanks.اتش_اس_بى_سى_HSBC:
                    return bankID = (int)DollarTodayBanks.HSBC_بنك;

                case (int)EgyptRatesBanks.البنك_الأهلى_الكويتى:
                    return bankID = (int)DollarTodayBanks.البنك_الاهلي_الكويتي_بيريوس;

                case (int)EgyptRatesBanks.البنك_الأهلي_المصري:
                    return bankID = (int)DollarTodayBanks.البنك_الاهلى_المصرى;

                case (int)EgyptRatesBanks.البنك_التجاري_الدولي:
                    return bankID = (int)DollarTodayBanks.البنك_التجارى_الدولى;

                case (int)EgyptRatesBanks.البنك_العربي_الأفريقي_الدولى:
                    return bankID = (int)DollarTodayBanks.البنك_العربى_الافريقى_الدولى;

                case (int)EgyptRatesBanks.البنك_المركزى_المصرى:
                    return bankID = (int)DollarTodayBanks.البنك_المركزي_المصري;

                case (int)EgyptRatesBanks.البنك_المصرى_الخليجى:
                    return bankID = (int)DollarTodayBanks.البنك_المصرى_الخليجى;

                case (int)EgyptRatesBanks.البنك_المصرى_لتنمية_الصادرات:
                    return bankID = (int)DollarTodayBanks.البنك_المصري_لتنمية_الصادرات;

                case (int)EgyptRatesBanks.المصرف_العربى_الدولى:
                    return bankID = (int)DollarTodayBanks.المصرف_العربي_الدولي;

                case (int)EgyptRatesBanks.المصرف_المتحد:
                    return bankID = (int)DollarTodayBanks.المصرف_المتحد;

                case (int)EgyptRatesBanks.بنك_الإستثمار_العربي_AIB:
                    return bankID = (int)DollarTodayBanks.بنك_الاستثمار_العربي;

                case (int)EgyptRatesBanks.بنك_الإسكندرية:
                    return bankID = (int)DollarTodayBanks.بنك_الاسكندرية;

                case (int)EgyptRatesBanks.بنك_البركة:
                    return bankID = (int)DollarTodayBanks.بنك_البركة;

                case (int)EgyptRatesBanks.بنك_التعمير_والإسكان:
                    return bankID = (int)DollarTodayBanks.بنك_الاسكان_والتعمير;

                case (int)EgyptRatesBanks.بنك_التنمية_الصناعية:
                    return bankID = (int)DollarTodayBanks.بنك_التنمية_الصناعية;

                case (int)EgyptRatesBanks.بنك_الكويت_الوطني_NBK:
                    return bankID = (int)DollarTodayBanks.بنك_الكويت_الوطني;

                case (int)EgyptRatesBanks.بنك_بلوم:
                    return bankID = (int)DollarTodayBanks.بنك_بلوم_مصر;

                case (int)EgyptRatesBanks.بنك_عودة:
                    return bankID = (int)DollarTodayBanks.بنك_عودة;

                case (int)EgyptRatesBanks.بنك_قناة_السويس:
                    return bankID = (int)DollarTodayBanks.بنك_قناة_السويس;

                case (int)EgyptRatesBanks.بنك_مصر:
                    return bankID = (int)DollarTodayBanks.بنك_مصر;

                case (int)EgyptRatesBanks.بنك_مصر_إيران_التنمية:
                    return bankID = (int)DollarTodayBanks.بنك_مصر_ايران_للتنمية;

                case (int)EgyptRatesBanks.كريدى_أجريكول:
                    return bankID = (int)DollarTodayBanks.كريدي_أجريكول;

                case (int)EgyptRatesBanks.مصرف_أبوظبى_الإسلامى:
                    return bankID = (int)DollarTodayBanks.مصرف_ابوظبي_الاسلامي;

                case (int)EgyptRatesBanks.بنك_المشرق:
                    return bankID = (int)DollarTodayBanks.بنك_المشرق;

                case (int)EgyptRatesBanks.بنك_الشركة_المصرفية_العربية_الدولية_SAIB:
                    return bankID = (int)DollarTodayBanks.بنك_الشركة_المصرفية_العربية_الدولية_SAIB;

                case (int)EgyptRatesBanks.البنك_العقاري_المصري_العربي:
                    return bankID = (int)DollarTodayBanks.البنك_العقاري_المصري_العربي;

                case (int)EgyptRatesBanks.البنك_الأهلي_اليوناني:
                    return bankID = (int)DollarTodayBanks.البنك_الأهلي_اليوناني;
                default:
                    return bankID;
            }
        }

        private void DeleteCurrencyByBank(int bankId, int currencyId)
        {
            try
            {
                using (var db = new EgyptCurrencyRatesContext())
                {
                    db.Rates.Remove(db.Rates.Where(r => r.CurrencyId == currencyId && r.BankId == bankId).FirstOrDefault());
                }
            }
            catch (Exception)
            {
            }
        }

        public void CleanDatabase()
        {
            using (EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext())
            {
                var rates = db.Rates.Where(r => r.Date.Day < DateTime.Now.Day).ToList();
                db.Rates.RemoveRange(rates);

                this.Start();
                db.SaveChanges();
            }
        }
    }
}