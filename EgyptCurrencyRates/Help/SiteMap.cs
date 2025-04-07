using EgyptCurrencyRates.Models;

namespace EgyptCurrencyRates.Help
{
    public class SiteMap
    {
        IWebHostEnvironment hosting;
        public SiteMap(IWebHostEnvironment _hosting)
        {
            hosting = _hosting;
        }

        public void SitemapGenerator_01()
        {
            string rootPath = Path.Combine(hosting.WebRootPath, "sitemap\\");

            string newFile = rootPath + "sitemap01.xml";

            if (!System.IO.File.Exists(newFile))
            {
                string tempFile = rootPath + "\\temp.xml";
                System.IO.File.Copy(tempFile, newFile);
            }

            DateTime date = DateTime.Now.AddDays(-1);
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();
            List<Article> articles = db.Articles.Where(a => a.Date > date).ToList();

            string newMapUrls = string.Empty;
            foreach (Article article in articles)
            {
                string url = @"https://egyptcurrencyrates.com/news/" + article.Title.Replace(" ", "-");

                string newMapUrl = @"<url>" + Environment.NewLine +
                                            "<loc>" + url + "</loc>" + Environment.NewLine + "" +
                                            "<lastmod>" + article.Date.Value.ToString("yyyy-MM-ddTHH:mm:sszzz") + "</lastmod>" + Environment.NewLine +
                                            "<priority>0.80</priority>" + Environment.NewLine +
                                            "</url>" + Environment.NewLine;
                newMapUrls += newMapUrl;
            }



            string newContent = string.Empty;
            using (StreamReader stream = new StreamReader(newFile))
            {
                string existContent = stream.ReadToEnd();

                string tempUrl = "<url><loc>https://egyptcurrencyrates.com/News/Index</loc></url>";

                newContent = existContent.Replace(tempUrl, tempUrl + Environment.NewLine + newMapUrls);
                stream.Close();
                stream.Dispose();
                System.IO.File.WriteAllText(newFile, newContent);
            }
        }

        public void SitemapGenerator_02()
        {
            string rootPath = Path.Combine(hosting.WebRootPath, "sitemap\\");

            string newFile = rootPath + "sitemap02.xml";

            if (!System.IO.File.Exists(newFile))
            {
                string tempFile = rootPath + "\\temp.xml";
                System.IO.File.Copy(tempFile, newFile);
            }

            DateTime date = DateTime.Now.AddDays(-1);
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();
            List<Article> articles = db.Articles.Where(a => a.Date > date && a.Ispermanent == false).ToList();

            string newMapUrls = string.Empty;
            foreach (Article article in articles)
            {
                string url = @"https://egyptcurrencyrates.com/news/" + article.Title.Replace(" ", "-");

                string newMapUrl = @"<url>" + Environment.NewLine +
                                            "<loc>" + url + "</loc>" + Environment.NewLine + "" +
                                            "<lastmod>" + article.Date.Value.ToString("yyyy-MM-ddTHH:mm:sszzz") + "</lastmod>" + Environment.NewLine +
                                            "<priority>0.80</priority>" + Environment.NewLine +
                                            "</url>" + Environment.NewLine;
                newMapUrls += newMapUrl;
            }



            string newContent = string.Empty;
            using (StreamReader stream = new StreamReader(newFile))
            {
                string existContent = stream.ReadToEnd();

                string tempUrl = "<url><loc>https://egyptcurrencyrates.com/News/Index</loc></url>";

                newContent = existContent.Replace(tempUrl, tempUrl + Environment.NewLine + newMapUrls);
                stream.Close();
                stream.Dispose();
                System.IO.File.WriteAllText(newFile, newContent);
            }
        }

        public void SitemapGenerator_03()
        {
            string rootPath = Path.Combine(hosting.WebRootPath, "sitemap\\");

            string newFile = rootPath + "sitemap03.xml";

            if (!System.IO.File.Exists(newFile))
            {
                string tempFile = rootPath + "\\temp.xml";
                System.IO.File.Copy(tempFile, newFile);
            }

            DateTime date = DateTime.Now.AddDays(-1);
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();

            List<Bank> banks = db.Banks.ToList();

            string newMapUrls = string.Empty;
            foreach (Bank bank in banks)
            {
                string url = @"https://egyptcurrencyrates.com/Bank/" + bank.Name.Replace(" ", "-");

                string newMapUrl = @"<url>" + Environment.NewLine +
                                            "<loc>" + url + "</loc>" + Environment.NewLine + "" +
                                            //"<lastmod>" + article.Date.Value.ToString("yyyy-MM-ddTHH:mm:sszzz") + "</lastmod>" + Environment.NewLine +
                                            //"<priority>0.80</priority>" + Environment.NewLine +
                                            "</url>" + Environment.NewLine;
                newMapUrls += newMapUrl;
            }

            List<Currency> currencies = db.Currencies.ToList();
            foreach (Currency currency in currencies)
            {
                string url = @"https://egyptcurrencyrates.com/Currency/" + currency.Name.Replace(" ", "-");

                string newMapUrl = @"<url>" + Environment.NewLine +
                                            "<loc>" + url + "</loc>" + Environment.NewLine + "" +
                                            //"<lastmod>" + article.Date.Value.ToString("yyyy-MM-ddTHH:mm:sszzz") + "</lastmod>" + Environment.NewLine +
                                            //"<priority>0.80</priority>" + Environment.NewLine +
                                            "</url>" + Environment.NewLine;
                newMapUrls += newMapUrl;
            }


            foreach (Rate rate in db.Rates.ToList())
            {
                //https://egyptcurrencyrates.com/Currency/الدولار/البنك-الأهلي-المصري
                string url = @"https://egyptcurrencyrates.com/Currency/" + rate.Currency.Name.Replace(" ", "-") + "/" + rate.Bank.Name.Replace(" ", "-");

                string newMapUrl = @"<url>" + Environment.NewLine +
                            "<loc>" + url + "</loc>" + Environment.NewLine + "" +
                            "</url>" + Environment.NewLine;
                newMapUrls += newMapUrl;
            }



            string newContent = string.Empty;
            using (StreamReader stream = new StreamReader(newFile))
            {
                string existContent = stream.ReadToEnd();

                string tempUrl = "<url><loc>https://egyptcurrencyrates.com/News/Index</loc></url>";

                newContent = existContent.Replace(tempUrl, tempUrl + Environment.NewLine + newMapUrls);
                stream.Close();
                stream.Dispose();
                System.IO.File.WriteAllText(newFile, newContent);
            }
        }

    }
}