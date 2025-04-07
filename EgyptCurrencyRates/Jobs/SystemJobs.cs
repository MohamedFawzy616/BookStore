using EgyptCurrencyRates.Crawler;
using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Services;
using Quartz;

namespace EgyptCurrencyRates.Jobs
{
    [ExceptionLog]
    [DisallowConcurrentExecution]
    public class RatesJob : IJob
    {
        readonly IWebHostEnvironment host;
        private readonly TelegramBotService botService;

        public RatesJob(IWebHostEnvironment _hosting, TelegramBotService _botService)
        {
            host = _hosting;
            botService = _botService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            EgyptRatesCrawler rates = new EgyptRatesCrawler();
            rates.Start();

            FileCreator utility = new FileCreator(host);
            utility.CreateRatesFiles();

            MediaManager mediaManger = new MediaManager(host);
            await mediaManger.PostToTelegram();
        }
    }


    [ExceptionLog]
    [DisallowConcurrentExecution]
    public class GoldJob : IJob
    {
        readonly IWebHostEnvironment host;
        private readonly TelegramBotService botService;
        public GoldJob(IWebHostEnvironment _hosting, TelegramBotService _botService)
        {
            host = _hosting;
            botService = _botService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            GoldCrawler crawler = new GoldCrawler();
            crawler.Goldrate24();

            FileCreator fileCreator = new FileCreator(host);
            fileCreator.CreateGoldFiles();
        }
    }
    

    [ExceptionLog]
    [DisallowConcurrentExecution]
    public class SiteMapJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap jonDataMap = context.JobDetail.JobDataMap;
            IWebHostEnvironment hosing = (IWebHostEnvironment)jonDataMap["host"];
            SiteMap siteMap = new SiteMap(hosing);
            siteMap.SitemapGenerator_01();
        }
    }


    [ExceptionLog]
    [DisallowConcurrentExecution]
    public class twitterGoldPostJob : IJob
    {
        readonly IWebHostEnvironment host;
        public twitterGoldPostJob(IWebHostEnvironment _hosting)
        {
            host = _hosting;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            MediaManager mediaManger = new MediaManager(host);
            await mediaManger.PostToTwitterGold();
        }
    }


    [ExceptionLog]
    [DisallowConcurrentExecution]
    public class CleanDatabase : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            EgyptRatesCrawler rates = new EgyptRatesCrawler();
            rates.CleanDatabase();
        }
    }
}