using Microsoft.AspNetCore.Mvc;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Filter;


namespace EgyptCurrencyRates.Controllers
{
    [ExceptionLog]
    public class GoldController : Controller
    {
        readonly IWebHostEnvironment host;
        public GoldController(IWebHostEnvironment _host)
        {
            host = _host;
        }

        [Route("Gold")]
        [Route("Gold/Index")]
        public ActionResult Index()
        {
            if (Request.Host.Value.Contains("dollar-today"))
            {
                return Redirect(string.Format("https://egyptcurrencyrates.com/Gold/index"));
            }

            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();


            //List<GoldPriceModel> goldPriceModels = db.GoldPrices.Select(static g => new GoldPriceModel
            //{
            //    Id = g.Id,
            //    UnitTypeId = g.GoldUnitNavigation.GoldUnitType.Value,
            //    GoldUnitTitleAr = g.GoldUnitNavigation.TitleAr ?? string.Empty,
            //    EgyptianPound = g.EgyptianPound ?? string.Empty,
            //    USADollar = g.UsaDollar ?? string.Empty,
            //    Date = g.Date,
            //}).ToList();


            FileModels fileModels = new FileModels(host);
            List<GoldPriceModel> goldPriceModels = fileModels.GoldPrices().Where(g => g.UnitTypeId == 1).Select(static g => new GoldPriceModel
            {
                Id = g.Id,

                EgyptianPound = g.EgyptianPound ?? string.Empty,
                USADollar = g.USADollar ?? string.Empty,
                Date = g.Date,

                //UnitTypeId = g.UnitTypeId,
                GoldUnitTitleAr = g.GoldUnitTitleAr,// g.GoldUnitNavigation.TitleAr ?? string.Empty,

            }).ToList();

            ViewBag.Title = string.Format("سعر الذهب اليوم - تحديث فوري");
            ViewBag.h2 = string.Format("متابعة مباشرة لاسعار الذهب فى مصر");
            ViewBag.h3 = string.Format("تحليل سعر الذهب في مصر");


            ViewBag.description = string.Format("تعرف على سعر الذهب اليوم فى مصر تحديث فوري");
            ViewBag.keywords = string.Format("سعر الذهب,سعر ذهب 21,سعر ذهب 24,أسعار الذهب محدثة,الذهب");
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            return View(goldPriceModels);
        }

        private dynamic CreateSummaryContent()
        {
            return @"
سعر الذهب اليوم,
سعر الذهب الآن,
سعر الذهب في مصر,
سعر الذهب مباشر,
أسعار الذهب اليوم في مصر,
سعر الذهب مباشر في مصر,
سعر الذهب عيار ٢١ اليوم,
توقعات أسعار الذهب,البنك الاهلى المصرى اسعار العملات,
اسعار العملات البنك الاهلى المصرى,
أسعار العملات بنك الإسكندرية,
سعر الدولار فى البنك العربى الافريقى,
سعر الدولار بنك الاسكندرية,
سعر الدولار اليوم فى بنك الاسكندرية,
سعر الدولار في بنك الاسكندرية,
اسعار العملات فى البنك الاهلى المصرى,
اسعار العملات فى البنك الاهلى,
سعر الدولار فى بنك اسكندرية,
سعر الدولار اليوم مصر بنك الإسكندرية,
سعر الدولار اليوم فى البنك العربى الافريقى,
سعر الدولار في بنك الاسكندرية اليوم,
سعر الدولار اليوم بنك الاسكندرية,
سعر الدولار اليوم فى بنك فيصل الاسلامي المصري,
اسعار العملات البنك الاهلى,
اسعار العملات بنك الاسكندرية,
سعر الدولار الكندي في بنك cib,
سعر الدولار اليوم فى بنك اسكندريه";
        }
    }
}