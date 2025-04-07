using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace EgyptCurrencyRates.Controllers
{
    [ExceptionLog]
    public class CurrencyController : Controller
    {
        readonly IWebHostEnvironment host;
        readonly IScheduler Scheduler;

        public CurrencyController(IWebHostEnvironment _host, IScheduler _Scheduler)
        {
            host = _host;
            Scheduler = _Scheduler;
        }

        [Route("/")]
        [Route("Currency")]
        [Route("Currency/{currency?}")]
        public IActionResult Currency(string currency = "الدولار")
        {
            //if (Request.Host.Value.Contains("dollar-today"))
            //{
            //    var uri = new Uri(string.Format("https://egyptcurrencyrates.com/Currency/" + currency));
            //    return Redirect(uri.AbsoluteUri);
            //}
            if (currency.Equals("-") || currency.Contains("'") || currency.Contains("„") || currency.Contains("%") || currency.Contains("™"))
            {
                return new NotFoundResult();
            }


            string CurrencyName = Switch.Currency(currency);

            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();

            int currencyId = 1;
            if (!currency.Equals("الدولار"))
            {
                currencyId = Currencies.FirstOrDefault(c => c.Name.Equals(CurrencyName)).ID;
            }

            List<RateViewModel> rates;
            using (var db = new EgyptCurrencyRatesContext())
            {
                //rates = db.Rate.Include("Currency").Include("Bank").Where(r => r.CurrencyId == currencyId).OrderByDescending(r => r.BuyPrice).ToList();
                rates = SQLQueryCurrency(currencyId);
            }

            //ViewBag.Banks = new EgyptCurrencyRatesContext().Bank.Where(b => b.Visible == true).ToList();
            //ViewBag.Currencies = new EgyptCurrencyRatesContext().Currency.Where(c => c.Visiable == true).ToList();
            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.Where(c => c.Visiable == true).ToList();

            ViewBags(CurrencyName, string.Empty, currencyId, 0);
            return View(rates);
        }

        public static List<RateViewModel> SQLQueryCurrency(int currency_ID)
        {
            List<RateViewModel> rates = new List<RateViewModel>();
            using (SqlConnection con = new SqlConnection(new EgyptCurrencyRatesContext().Connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @$"select 
Bank.Name Bank,Bank.Logo BankLogo,Currency.Name Currency,Currency.Code  CurrencyCode,Buy_Price,Sale_Price,Date
from Rate
inner join Currency on Currency.ID = Rate.Currency_ID
inner join Bank     on Bank.ID     = Rate.Bank_ID and Rate.Currency_ID = {currency_ID}
order by Buy_Price desc";


                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandTimeout = 0;

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var rate = new RateViewModel
                        {
                            Bank = Convert.ToString(dataReader["Bank"]),
                            BankLogo = Convert.ToString(dataReader["BankLogo"]),
                            Currency = Convert.ToString(dataReader["Currency"]),
                            CurrencyCode = Convert.ToString(dataReader["CurrencyCode"]),
                            Buy_Price = Convert.ToDouble(dataReader["Buy_Price"]),
                            Sale_Price = Convert.ToDouble(dataReader["Sale_Price"]),
                            Date = Convert.ToDateTime(dataReader["Date"]),
                        };
                        rates.Add(rate);
                    }
                }
                return rates;
            }
        }


        [Route("Currency/{Currency}/{bank}")]
        public IActionResult Currency(string Currency, string bank)
        {
            if (string.IsNullOrWhiteSpace(Currency) && string.IsNullOrWhiteSpace(bank))
            {
                Currency = "الدولار";
            }

            if (bank.Contains("اليوناني") && bank.Contains("الأهلي") && bank.Contains("البنك") || 
                (bank.Contains("مصر") && bank.Contains("بلوم") && bank.Contains("بنك")))
            {
                var uri = new Uri(string.Format("https://egyptcurrencyrates.com/Currency/" + Currency));
                return Redirect(uri.AbsoluteUri);
            }

      
            if (Currency.Equals("-") || Currency.Contains("'") || Currency.Contains("„") || Currency.Contains("%") || Currency.Contains("™"))
            {
                return new NotFoundResult();
            }


            string currencyName = Switch.Currency(Currency);
            string bankName = Switch.Bank(bank);



            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();

            int CurrencyId = 1;
            if (!Currency.Equals("الدولار"))
            {
                CurrencyId = Currencies.FirstOrDefault(c => c.Name.Equals(currencyName)).ID;
            }
            int bankId = Banks.FirstOrDefault(b => b.Name.Equals(bankName)).Id;

            Rate rate;
            using (var db = new EgyptCurrencyRatesContext())
            {
                rate = db.Rates.Include("Currency").Include("Bank").Where(r => r.CurrencyId == CurrencyId && r.BankId == bankId).FirstOrDefault();
            }

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.Where(c => c.Visiable == true).ToList();
            ViewBag.BankName = bankName;

            ViewBags(currencyName, bankName, CurrencyId, bankId);


            List<RateViewModel> rateViewModels = new List<RateViewModel>
            {
            new RateViewModel
            {
                    Bank = Convert.ToString(rate.Bank.Name),
              CurrencyLogo = Convert.ToString(rate.Currency.Logo),
                BankLogo = Convert.ToString(rate.Bank.Logo),
                Currency = Convert.ToString(rate.Currency.Name),
                Buy_Price = Convert.ToDouble(rate.BuyPrice),
                Sale_Price = Convert.ToDouble(rate.SalePrice),
                Date = Convert.ToDateTime(rate.Date),
            }

            };

            return View(rateViewModels);
        }

        [Route("/{title}")]
        public IActionResult Currency()
        {
            return null;
            //if (Request.Host.Value.Contains("dollar-today"))
            //{
            //    var uri = new Uri(string.Format("https://egyptcurrencyrates.com/Currency/" + currency));
            //    return Redirect(uri.AbsoluteUri);
            //}
            //if (currency.Equals("-") || currency.Contains("'") || currency.Contains("„") || currency.Contains("%") || currency.Contains("™"))
            //{
            //    return new NotFoundResult();
            //}


            //string CurrencyName = Switch.Currency(currency);

            //FileModels fileModels = new FileModels(host);

            //List<CurrencyViewModel> Currencies = fileModels.Currencies();
            //List<BankViewModel> Banks = fileModels.Banks();

            //int currencyId = 1;
            //if (!currency.Equals("الدولار"))
            //{
            //    currencyId = Currencies.FirstOrDefault(c => c.Name.Equals(CurrencyName)).ID;
            //}

            //List<Rate> rates;
            //using (var db = new EgyptCurrencyRatesContext())
            //{
            //    rates = db.Rate.Include("Currency").Include("Bank").Where(r => r.CurrencyId == currencyId).OrderByDescending(r => r.BuyPrice).ToList();
            //}

            //ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            //ViewBag.Currencies = Currencies.Where(c => c.Visiable == true).ToList();

            //ViewBags(rates, CurrencyName, string.Empty, currencyId, 0);
            //return View(rates);
        }



        private void ViewBags(string CurrencyName, string BankName, int CurrencyId, int BankId)
        {
            if (!string.IsNullOrWhiteSpace(BankName))
            {
                ViewBag.Title = string.Format("سعر {0} اليوم في {1} - تحديث فوري", CurrencyName, BankName);
                ViewBag.description = string.Format("تعرف على سعر {0} اليوم في {1} بتحديث لحظي، واستفد من متابعة دقيقة لأسعار الصرف مقابل الجنيه المصري.", CurrencyName, BankName);
                ViewBag.keywords = string.Format("سعر {0} اليوم , {1} , أسعار العملات", CurrencyName, BankName);

                ViewBag.h2 = string.Format("تحديث  سعر {0} في {1}", CurrencyName, BankName);
                ViewBag.h3 = string.Format("تحديث سعر {0} في {1}", CurrencyName, BankName);

                CurrencyBankSummary(CurrencyId, BankId);
            }
            else
            {
                ViewBag.Title = string.Format("سعر {0} اليوم - تحديث فوري", CurrencyName);
                ViewBag.h2 = string.Format("سعر {0} في البنوك المصرية", CurrencyName);
                ViewBag.h3 = string.Format("تحديث سعر {0} في البنوك المصرية", CurrencyName);


                ViewBag.description = string.Format("تعرف على سعر {0} اليوم في مصر محدث يوميًا. أسعار العملات في البنوك المصرية مثل البنك الأهلي، بنك الإسكندرية، والبنك المركزي. تحديث فوري", CurrencyName);
                ViewBag.keywords = string.Format("سعر {0} اليوم ,سعر {0} في البنك الأهلي المصري ,سعر {0} في بنك الإسكندرية ,سعر {0} في بنك فيصل الإسلامي المصري ,سعر {0} في البنك العربي الأفريقي", CurrencyName);


                //ViewBag.description = string.Format("سعر {0} اليوم سعر {0} في البنك الأهلي المصري وسعر {0} في بنك الإسكندرية وسعر {0} في بنك فيصل الإسلامي المصري وسعر {0} في البنك العربي الأفريقي", CurrencyName);
                //ViewBag.keywords = string.Format("سعر {0} اليوم ,سعر {0} في البنك الأهلي المصري ,سعر {0} في بنك الإسكندرية ,سعر {0} في بنك فيصل الإسلامي المصري ,سعر {0} في البنك العربي الأفريقي", CurrencyName);
            }
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);
        }

        private void CurrencyBankSummary(int CurrencyId, int BankId)
        {
            var db = new EgyptCurrencyRatesContext();

            Rate rate = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == BankId).FirstOrDefault();

            string CurrencyName = rate.Currency.Name;
            string BankName = rate.Bank.Name;
            string Day = Analytics.Day();

            string Date = rate.Date.ToString("dd MMMM yyyy", new CultureInfo("ar-EG"));

            string BuyPrice = rate.BuyPrice.ToString();
            string SalePrice = rate.SalePrice.ToString();


            StringBuilder stringBuilder = new StringBuilder();

            /*
              سعر صرف الريال السعودي فى بنك الإسكندرية يوم السبت الموافق 22 فبراير 2025 ، أعلن بنك الإسكندرية عن أسعار الصرف،
              حيث بلغ سعر الشراء 13.441 جنيه مصري، بينما كان سعر البيع 13.495 جنيه. وفي المقابل ،
              سجل مصرف أبو ظبي الإسلامي أعلى سعر شراء الريال السعودي عند 13.489 جنيه،
              في حين قدم البنك المصري الخليجي أقل سعر بيع الريال السعودي عند 13.47 جنيه. من جهة أخرى،
              أعلن البنك المركزي المصري عن سعر شراء الريال السعودي عند 13.465 جنيه، وسعر البيع عند 13.465 جنيه.

 */

            stringBuilder.AppendLine(string.Format(@"سعر صرف {0} فى <a href='/Bank/{4}' target='_blank'>{1}</a> يوم {2} الموافق {3} ،", CurrencyName, BankName, Day, Date, BankName.Replace(" ", "-")));
            stringBuilder.AppendLine(string.Format(@"أعلن <a href='/Bank/{4}' target='_blank'>{0}</a> عن أسعار الصرف، حيث بلغ سعر الشراء {2} جنيه مصري، بينما كان سعر البيع {3} جنيه. وفي المقابل ،", BankName, CurrencyName, BuyPrice, SalePrice, BankName.Replace(" ", "-")));


            var HighBuyBank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId != 9).OrderByDescending(r => r.BuyPrice).FirstOrDefault();
            var LawSaleBank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId != 9).OrderBy(r => r.SalePrice).FirstOrDefault();

            string HighBuyBankName = HighBuyBank.Bank.Name;
            string HighBuyBankPrice = HighBuyBank.BuyPrice.ToString();

            string LawSaleBankName = LawSaleBank.Bank.Name;
            string lawBuyBankPrice = LawSaleBank.SalePrice.ToString();

            stringBuilder.AppendLine(string.Format(@"سجل <a href='/Bank/{5}' target='_blank'>{0}</a> أعلى سعر شراء {1} عند {2} جنيه، في حين قدم <a href='/Bank/{6}' target='_blank'>{3}</a> أقل سعر بيع {1} عند {4} جنيه.", HighBuyBankName, CurrencyName, HighBuyBankPrice, LawSaleBankName, lawBuyBankPrice, HighBuyBankName.Replace(" ", "-"), LawSaleBankName.Replace(" ", "-")));


            Rate CentralBank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == 8).FirstOrDefault();
            if (CentralBank != null)
            {
                string CentralBankBuyPrice = CentralBank.BuyPrice.ToString();
                string CentralBankSalePrice = CentralBank.BuyPrice.ToString();
                stringBuilder.AppendLine(string.Format("من جهة أخرى، أعلن <a href=\'/Bank/البنك-المركزي-المصري\' target=\'_blank\'>البنك المركزي المصري</a> عن سعر شراء الريال السعودي عند {0} جنيه، وسعر البيع عند {1} جنيه.", CentralBankBuyPrice, CentralBankSalePrice));
            }

            string Summary = stringBuilder.ToString();

            ViewBag.Summary = Summary;
        }

        private void Serialization(List<Rate> rates)
        {
            List<object> rateListElement = new List<object>();
            foreach (var rate in rates)
            {
                rateListElement.Add(new { @type = "ExchangeRateSpecification", currency = rate.Currency.Name, currentExchangeRate = new { @type = "UnitPriceSpecification", price = rate.BuyPrice/*, saleprice = rate.Sale_Price, date = string.Format("{0:t}", rate.Date)*/ } });
            }
            ViewBag.rateListElement_jsonLd = JsonConvert.SerializeObject(rateListElement);
        }

        [Route("Currency/Policy")]
        public ActionResult Policy()
        {
            ViewBag.Title = "سياسة موقع egyptcurrencyrates.com";
            ViewBag.description = "الموقع لا يروج لاى بنك من البنوك ولا يهدف الى تجارة العملة يهدف الى توفير المعلومة للمستخدم فقط";

            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.ToList();
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            return View();
        }

        [Route("Currency/Privacy")]
        public ActionResult Privacy()
        {
            ViewBag.Title = "سياسة الخصوصية";
            ViewBag.description = "نحن لا نجمع معلومات عنك من اى نوع ، كما ان الموقع لا يقوم بوضع اى ملفات كوكيز على جهازك الشحصى";

            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.ToList();
            return View();
        }

        [Route("Currency/About")]
        public ActionResult About()
        {
            ViewBag.Title = "About egyptcurrencyrates.com";
            ViewBag.description = "موقع أسعار العملات في مصر هدفه هو توفير أسعار صرف العملات المختلفة فى جميع البنوك المصرية";

            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();
            ViewBag.IsMobileDevice = RequestExtensions.IsMobileBrowser(Request);

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.ToList();
            return View();
        }

        public void Bookmark(string city, string country, string hostname, string ip, string loc, string org, string region, bool bookMark)
        {
            //using (var db = new EgyptCurrencyRatesContext())
            //{
            //    if (!(db.User.Where(u => u.Ip.Equals(ip) && u.BookMark.Value.Equals(bookMark)).Count() > 0))
            //    {
            //        db.User.Add(new User { Ip = ip, City = city, Country = country, Hostname = hostname, Loc = loc, Org = org, Region = region, BookMark = bookMark });
            //        db.SaveChanges();
            //    }
            //}
        }
    }
}