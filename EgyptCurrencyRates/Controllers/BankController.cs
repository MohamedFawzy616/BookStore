using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Help;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EgyptCurrencyRates.Controllers
{
    [ExceptionLog]
    public class BankController : Controller
    {
        readonly IWebHostEnvironment host;
        public BankController(IWebHostEnvironment _host)
        {
            host = _host;
        }


        [Route("Bank/test")]
        public ActionResult file()
        {
            FileCreator creator = new FileCreator(host);
            creator.Currencies();
            return null;
        }


        [Route("Bank/{bank?}")]
        public ActionResult Bank(string bank)
        {
            //if (Request.Host.Value.Contains("dollar-today"))
            //{
            //    var uri = new Uri(string.Format("https://egyptcurrencyrates.com/Bank/" + bank));
            //    return Redirect(uri.AbsoluteUri);
            //}
            if (bank.Equals("-") || bank.Contains("'") || bank.Contains("„") || bank.Contains("%") || bank.Contains("™"))
            {
                return new NotFoundResult();
            }

            bank = Switch.Bank(bank);

            FileModels fileModels = new FileModels(host);

            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();


            int BankId = Banks.FirstOrDefault(b => b.Name.Equals(bank)).Id;

            List<RateViewModel> Rates;

            using (var db = new EgyptCurrencyRatesContext())
            {
                //Rates = db.Rate.Include("Bank").Include("Currency").Where(r => r.BankId == BankId).OrderBy(r => r.Currency.Order).ToList();
                Rates = SQLQueryBank(BankId);
            }

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.Where(c => c.Visiable == true).ToList();

            ViewBags(bank, string.Empty, 0, BankId);
            return View(Rates);
        }

        public static List<RateViewModel> SQLQueryBank(int bank_id)
        {
            List<RateViewModel> rates = new List<RateViewModel>();
            using (SqlConnection con = new(new EgyptCurrencyRatesContext().Connection))
            {
                con.Open();
                SqlCommand cmd = new();
                cmd.CommandText = @$"select 
Bank.Name Bank,Currency.Logo CurrencyLogo,Currency.Name Currency,Code CurrencyCode,Buy_Price,Sale_Price,Date
from Rate
inner join Currency on Currency.ID = Rate.Currency_ID
inner join Bank     on Bank.ID     = Rate.Bank_ID and Rate.Bank_ID = {bank_id}
order by Currency.[Order]";


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
                            CurrencyLogo = Convert.ToString(dataReader["CurrencyLogo"]),
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

        [Route("Bank/{bank}/{currency}")]
        public ActionResult Bank(string bank, string currency)
        {
            string concounil = "https://egyptcurrencyrates.com/Currency/" + currency.Replace(" ", "-") + "/" + bank.Replace(" ", "-");

            //https://localhost:44367/Bank/بنك-عودة/الدولار
            //if (Request.Host.Value.Contains("dollar-today"))
            //{
            //    var uri = new Uri(string.Format("https://egyptcurrencyrates.com/Bank/" + bank));
            //    return Redirect(uri.AbsoluteUri);
            //}

            if (bank.Contains("اليوناني") && bank.Contains("الأهلي") && bank.Contains("البنك") ||
                (bank.Contains("مصر") && bank.Contains("بلوم") && bank.Contains("بنك")))
            {
                var uri = new Uri(string.Format("https://egyptcurrencyrates.com"));
                return Redirect(uri.AbsoluteUri);
            }

            if (bank.Equals("-") || bank.Contains("'") || bank.Contains("„") || bank.Contains("%") || bank.Contains("™"))
            {
                return new NotFoundResult();
            }

            bank = Switch.Bank(bank);

            FileModels fileModels = new FileModels(host);
            List<CurrencyViewModel> Currencies = fileModels.Currencies();
            List<BankViewModel> Banks = fileModels.Banks();

            string CurrencyName = currency.Replace("-", " ");

            int BankId = Banks.FirstOrDefault(b => b.Name.Equals(bank)).Id;
            int CurrencyId = Currencies.FirstOrDefault(c => c.Name.Equals(CurrencyName)).ID;

            Rate rate;
            using (var db = new EgyptCurrencyRatesContext())
            {
                rate = db.Rates.Include("Bank").Include("Currency").Where(r => r.BankId == BankId && r.CurrencyId == CurrencyId).FirstOrDefault();
            }

            ViewBag.Banks = Banks.Where(b => b.Visible == true).ToList();
            ViewBag.Currencies = Currencies.Where(c => c.Visiable == true).ToList();

            ViewBags(bank, CurrencyName, CurrencyId, BankId);

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



        private void ViewBags(string BankName, string CurrencyName, int CurrencyId, int BankId)
        {
            if (!string.IsNullOrWhiteSpace(CurrencyName))
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
                ViewBag.Title = string.Format("أسعار العملات فى {0} اليوم - تحديث فوري", BankName);
                ViewBag.h2 = string.Format("تحديث أسعار العملات فى {0}", BankName);
                ViewBag.h3 = string.Format("تحديث أسعار العملات فى {0}", CurrencyName);


                ViewBag.description = string.Format("أسعار العملات في {0} اليوم وسعر الدولار في {0} وسعر اليورو في {0} وسعر الريال السعودي في {0} وسعر الريال القطري في {0} تحديث فوري", BankName);
                ViewBag.keywords = string.Format("أسعار العملات في {0} اليوم , سعر الدولار في {0} , سعر اليورو في {0} , سعر الريال السعودي في {0} , سعر الريال القطري في {0}", BankName);
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



إذا كنت تتابع سعر الدولار الأمريكي مقابل الجنيه المصري ،
           فإن بنك الإسكندرية يعد أحد الخيارات المفضلة للكثيرين. في هذه الصفحة،
            نقدم لك تحديثات يومية لأسعار الصرف، سواء للشراء أو البيع،
            مع معلومات دقيقة تساعدك على اتخاذ القرار المناسب.




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
    }
}