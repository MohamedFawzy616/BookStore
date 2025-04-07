using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Jobs;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EgyptCurrencyRates.Controllers
{
    public class HomeController : Controller
    {
        readonly IWebHostEnvironment host;
        readonly IScheduler Scheduler;

        public HomeController(IWebHostEnvironment _host, IScheduler _Scheduler)
        {
            host = _host;
            Scheduler = _Scheduler;
        }


        public IActionResult Sitemap()
        {

            Dictionary<string, IWebHostEnvironment> dictionary = new Dictionary<string, IWebHostEnvironment>();
            dictionary.Add("host", host);
            JobDataMap data = new JobDataMap(dictionary);

            #region SiteMap
            IJobDetail SiteMapJobcreate = JobBuilder.Create<SiteMapJob>()
                .UsingJobData(data)
                .WithIdentity("SiteMapJobcreate", "Group5")
                .Build();

            ITrigger TrSiteMapJobcreate = TriggerBuilder.Create()
                .WithIdentity("TrSiteMapJobcreate", "Group5")
                .WithCronSchedule("0 20 17 * * ?")

                .WithSimpleSchedule(s => s.RepeatForever())
                .Build();

            Scheduler.ScheduleJob(SiteMapJobcreate, TrSiteMapJobcreate);
            #endregion

            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }




        public IActionResult DeleteArticle()
        {
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();

            var articles = db.Articles.ToList();

            db.Articles.RemoveRange(articles);
            db.SaveChanges();
            /*
             News/Index
             */
            return RedirectToAction("Index", "News");

        }


        [Route("Home")]
        [Route("Home/{Index}")]
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [Route("Home/Update")]
        public IActionResult Update()
        {
            FileCreator files = new FileCreator(host);
            files.CreateGoldFiles();

            ViewBag.Title = "سياسة الخصوصية";
            ViewBag.description = "نحن لا نجمع معلومات عنك من اى نوع ، كما ان الموقع لا يقوم بوضع اى ملفات كوكيز على جهازك الشحصى";

            FileModels fileModels = new FileModels(host);
            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.ToList();
            return RedirectToAction("Currency", "Currency", new { name = "الدولار" });
        }

        [Route("Home/Integration")]
        public ActionResult Integration()
        {
            ViewBag.Title = "الاسعار لموقعك";
            ViewBag.description = "يتيح لك موقع أسعار العملات في مصر أسعار العملات فى مختلف البنوك المصرية وأسعار الذهب بشكل مجاني";

            FileModels fileModels = new FileModels(host);
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}