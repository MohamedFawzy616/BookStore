using EgyptCurrencyRates.Crawler;
using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgyptCurrencyRates.Controllers
{

    [ExceptionLog]
    public class xyzController : Controller
    {
        readonly IWebHostEnvironment host;
        private readonly TelegramBotService botService;

        public xyzController(IWebHostEnvironment _hosting, TelegramBotService _botService)
        {
            host = _hosting;
            botService = _botService;
        }

        int headerId = 1; int bodyId = 1; int footerId = 1;
        public async Task Post()
        {

            //MediaManager mediaManger = new MediaManager(host);
            //await mediaManger.PostToTwitterGold();
            /*
            FileModels fileModels = new FileModels(host);

            CounterViewModel counter = fileModels.LoadCounter();
            headerId = int.Parse(counter.headerId);
            bodyId = int.Parse(counter.bodyId);
            footerId = int.Parse(counter.footerId);

            PostViewModel header = fileModels.Header().First(a => a.Id == headerId);
            PostViewModel body = fileModels.Body().First(a => a.Id == bodyId);
            PostViewModel footer = fileModels.Footer().First(a => a.Id == footerId);



            List<GoldPriceModel> GoldPrices = fileModels.GoldPrices().Where(g => g.UnitTypeId == 1).ToList();

            CultureInfo culture = new CultureInfo("ar-EG");

            string formattedDate = GoldPrices.FirstOrDefault().Date.Value.ToString("yyyy MMMM dd", culture);

            string k24 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 2).EgyptianPound;
            string k22 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 3).EgyptianPound;
            string k21 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 4).EgyptianPound;
            string k18 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 5).EgyptianPound;
            string k14 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 6).EgyptianPound;

            body.Value = body.Value.Replace("@date", formattedDate).Replace("@k24", k24).Replace("@k21", k21).Replace("@k18", k18).Replace("@k14", k14);

            string post = header.Value + Environment.NewLine + Environment.NewLine +
                body.Value + Environment.NewLine + Environment.NewLine +
                footer.Value + Environment.NewLine;

            headerId++; bodyId++; footerId++;

            if (headerId >= 32) { headerId = 1; }
            if (bodyId >= 3) { bodyId = 1; }
            if (footerId >= 20) { footerId = 1; }


            var creator = new FileCreator(host);

            creator.SaveCounter(new CounterViewModel { headerId = headerId.ToString(), bodyId = bodyId.ToString(), footerId = footerId.ToString() });


            #region Telegram
            string botToken = "7548174039:AAGp9nuyLv8ygR52g7oILNKBAGripyz4Arc";
            string chatId = "-1002481245413";
            string channelId = "-1002305568159";
            string message = post;

            string group_url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";
            string channedl_url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={channelId}&text={Uri.EscapeDataString(message)}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage group_response = await client.GetAsync(group_url);
                await group_response.Content.ReadAsStringAsync();

                HttpResponseMessage channel_response = await client.GetAsync(channedl_url);
                await channel_response.Content.ReadAsStringAsync();
            }
            #endregion
            */
        }



        [Route("xyz/Rate")]
        public IActionResult Rate()
        {
            EgyptRatesCrawler rates = new EgyptRatesCrawler();
            rates.Start();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Ratefile()
        {
            FileCreator utility = new FileCreator(host);
            utility.CreateRatesFiles();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }


        [Route("xyz/Gold")]
        public IActionResult Gold()
        {
            GoldCrawler gold = new GoldCrawler();
            gold.Goldrate24();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        [Route("xyz/Goldfile")]
        public IActionResult Goldfile()
        {
            FileCreator utility = new FileCreator(host);
            utility.CreateGoldFiles();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }
        public IActionResult Currencies()
        {
            FileCreator utility = new FileCreator(host);
            utility.Currencies();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Banks()
        {
            FileCreator utility = new FileCreator(host);
            utility.Banks();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Clean()
        {
            EgyptRatesCrawler rates = new EgyptRatesCrawler();
            rates.CleanDatabase();

            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });

        }

        public IActionResult Article()
        {
            PermanentArticle permanentArticle = new PermanentArticle();
            permanentArticle.Article_Currencies();
            permanentArticle.Article_Banks();
            permanentArticle.Article_CurrenciesInBanks();
            permanentArticle.Article_Buy();
            permanentArticle.Article_Sale();


            TemporaryArticle temporaryArticle = new TemporaryArticle();
            temporaryArticle.Article_Currencies();
            temporaryArticle.Article_Banks();
            temporaryArticle.Article_CurrenciesInBanks();
            temporaryArticle.Article_Buy();
            temporaryArticle.Article_Sale();


            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Sitemap01()
        {
            //SiteMap siteMap = new SiteMap(hosting);
            //siteMap.SitemapGenerator_01();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Sitemap02()
        {
            SiteMap siteMap = new SiteMap(host);
            siteMap.SitemapGenerator_02();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        public IActionResult Sitemap03()
        {
            SiteMap siteMap = new SiteMap(host);
            siteMap.SitemapGenerator_03();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }
    }
}