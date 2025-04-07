using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Collections.Generic;
using System.Linq;


namespace EgyptCurrencyRates.Controllers
{
    public class NewsController : Controller
    {
        readonly IWebHostEnvironment host;
        readonly IScheduler Scheduler;
        public NewsController(IWebHostEnvironment _host, IScheduler _Scheduler)
        {
            host = _host;
            Scheduler = _Scheduler;
        }



        [Route("NewsList")]
        [Route("NewsList/Index")]
        [Route("NewsList/{type?}")]
        public async System.Threading.Tasks.Task<IActionResult> Index(string type, int page = 1)
        {
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();

            IQueryable<ArticleViewModel> articles;
            int TypeId = 0;
            if (type == "عملات")
            {
                TypeId = 1;
            }
            else if (type == "بنوك")
            {
                TypeId = 2;
            }
            else if (type == "عملات_و_بنوك")
            {
                TypeId = 3;
            }
            else if (type == "شراء")
            {
                TypeId = 4;
            }
            else if (type == "بيع")
            {
                TypeId = 5;
            }

            if (TypeId == 0)
            {
                articles = db.Articles
                     .Select(a => new ArticleViewModel() { Id = a.Id, Title = a.Title, Summary = a.Summary, Image = a.Image, Date = a.Date, Ispermanent = a.Ispermanent })
                     .OrderByDescending(i => i.Ispermanent);

                var models = await PaginatedList<ArticleViewModel>.CreateAsync(articles, page, 30);
                return View(models);
            }
            else
            {
                articles = db.Articles.Where(a => a.TypeId == TypeId)
                     .Select(a => new ArticleViewModel() { Id = a.Id, Title = a.Title, Summary = a.Summary, Image = a.Image, Date = a.Date, Ispermanent = a.Ispermanent })
                     .OrderByDescending(i => i.Ispermanent);

                var models = await PaginatedList<ArticleViewModel>.CreateAsync(articles, page, 30);
                return View(models);
            }
        }


        [Route("news/{title}")]
        public IActionResult Article(string title)
        {
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();

            Article article = db.Articles.FirstOrDefault(a => a.Title.Equals(title.Trim().Replace("-", " ")));
            ArticleViewModel model = new ArticleViewModel()
            {
                Id = article.Id,
                Image = article.Image,
                Content = article.Content,
                Description = article.Description,
                Keywords = article.Keywords,
                Date = article.Date,
                Title = article.Title
            };


            ViewBag.description = article.Description;
            ViewBag.keywords = article.Keywords;

            //ص  11:47 ص, السبت, 17 يوليو 21

            ViewBag.stringDateTime = Analytics.ConvertDate(article.Date.Value);


            ViewBag.seeMore = db.Articles.Where(a => a.TypeId == article.TypeId).Take(6);

            ViewBag.seeAlso = db.Articles.Where(a => a.TypeId == (article.TypeId == 3 ? 1 : 2)).Take(6);


            FileModels fileModels = new FileModels(host);


            ViewBag.Banks = fileModels.Banks().Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = fileModels.Currencies().Where(c => c.Visiable == true).ToList();
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            return View(model);
        }

        [Route("news/{id}/{title}")]
        public IActionResult Article(int id, string title)
        {
            EgyptCurrencyRatesContext db = new EgyptCurrencyRatesContext();

            Article article = db.Articles.Find(id);
            ArticleViewModel model = new ArticleViewModel()
            {
                Id = article.Id,
                Image = article.Image,
                Content = article.Content,
                Description = article.Description,
                Keywords = article.Keywords,
                Date = article.Date,
                Title = article.Title,
                Ispermanent = article.Ispermanent
            };

            ViewBag.description = article.Description;
            ViewBag.keywords = article.Keywords;

            ViewBag.stringDateTime = Analytics.ConvertDate(article.Date.Value);

            ViewBag.seeMore = db.Articles.Where(a => a.TypeId == article.TypeId /*&& article.Ispermanent == true*/).Take(6);

            ViewBag.seeAlso = db.Articles.Where(a => a.TypeId != article.TypeId /*&& article.Ispermanent == true*/).Take(6);

            FileModels fileModels = new FileModels(host);

            ViewBag.Banks = fileModels.Banks().Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = fileModels.Currencies().Where(c => c.Visiable == true).ToList();
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            return View(model);
        }
    }
}