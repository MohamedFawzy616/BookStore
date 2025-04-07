using EgyptCurrencyRates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptCurrencyRates.Help
{
    public class TemporaryArticle
    {
        public void Article_Currencies()
        {
            var db = new EgyptCurrencyRatesContext();

            foreach (var currency in db.Currencies)
            {
                var BuyPrice = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id).OrderByDescending(r => r.BuyPrice);
                var SalePrice = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id).OrderByDescending(r => r.SalePrice);

                if (BuyPrice.Count() < 1 || SalePrice.Count() < 1)
                {
                    continue;
                }

                Rate High_Buy_Price_Rate = BuyPrice.FirstOrDefault();
                double High_Buy_Price = High_Buy_Price_Rate.BuyPrice;
                string High_Buy_Bank_Name = High_Buy_Price_Rate.Bank.Name;

                Rate High_Sale_Rate = SalePrice.FirstOrDefault();
                double High_Sale_Price = High_Sale_Rate.SalePrice;
                string High_Sale_Price_Name = High_Sale_Rate.Bank.Name;

                Rate Law_Buy_Price_Rate = BuyPrice.OrderBy(r => r.BuyPrice).FirstOrDefault();
                double Law_Buy_Price = Law_Buy_Price_Rate.BuyPrice;
                string Law_Buy_Price_name = Law_Buy_Price_Rate.Bank.Name;

                Rate Law_Sale_Price_Rate = SalePrice.OrderBy(r => r.SalePrice).FirstOrDefault();
                double Law_Sale_Price = Law_Sale_Price_Rate.SalePrice;
                string Law_Sale_Price_name = Law_Sale_Price_Rate.Bank.Name;

                string content = string.Format(@"بلغ أعلى سعر لشراء {0} {3} في {4} وأعلى سعر لبيع {0} {5} في {6}
                وأقل سعر لشراء{0} {7} في {8} وأقل سعر لبيع {0} {9} في {10}.
                <h2>فيما يلي بيان تفصيلي بأسعار {0} في جميع البنوك المصرية</h2>" + Environment.NewLine, currency.Name,
                Analytics.Day(), Analytics.Date(), High_Buy_Price, High_Buy_Bank_Name, High_Sale_Price, High_Sale_Price_Name,
                Law_Buy_Price, Law_Buy_Price_name, Law_Sale_Price, Law_Sale_Price_name);

                string summary = string.Format(@"بلغ أعلى سعر لشراء {0} {3} في {4} وأعلى سعر لبيع {0} {5} في {6}
                وأقل سعر لشراء{0} {7} في {8} وأقل سعر لبيع {0} {9} في {10}." + Environment.NewLine, currency.Name,
                Analytics.Day(), Analytics.Date(), High_Buy_Price, High_Buy_Bank_Name, High_Sale_Price, High_Sale_Price_Name,
                Law_Buy_Price, Law_Buy_Price_name, Law_Sale_Price, Law_Sale_Price_name);


                string rates = string.Empty;
                foreach (var rate in db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id))
                {
                    rates += string.Format("سعر شراء {0} فى {1} {2} وسعر بيع {0} فى {1} {3} . {4} &",
                                      rate.Currency.Name,
                                      rate.Bank.Name,
                                      rate.BuyPrice,
                                      rate.SalePrice, Environment.NewLine);
                }


                content += rates;
                int i = content.Length;
                content = content.Remove(i - 4);
                Article article = new Article()
                {
                    Title = string.Format(@"أسعار {0} في البنوك المصرية صباح اليوم {1} الموافق {2}", currency.Name, Analytics.Day(), Analytics.Date()),
                    Content = content,
                    Summary = summary,
                    Description = string.Format(@"أسعار {0} في البنوك المصرية صباح اليوم {1} الموافق {2}", currency.Name, Analytics.Day(), Analytics.Date()),
                    Keywords = string.Format(@"أسعار {0} في البنوك المصرية , أسعار {0} اليوم ,{0} , سعر {0} اليوم", currency.Name),
                    Date = DateTime.Now,
                    Image = "currency_" + currency.Id + ".jpg",
                    TypeId = 1,
                    CurrencyId = currency.Id,
                    Ispermanent = false
                };
                db.Articles.Add(article);
            }
            db.SaveChanges();
        }

        public void Article_Banks()
        {
            var db = new EgyptCurrencyRatesContext();

            foreach (var bank in db.Banks.Where(b => b.Visible == true))
            {
                string content = string.Format(@"{0} أسعار العملات في {1} صباح اليوم {2} الموافق {3}
وباقي أسعار العملات في مختلف البنوك التي تعمل في السوق المصري ذلك بمقارنة أسعارالعملات المعلنة على موقع {1} وباقي البنوك.
وكانت أسعار العملات في {1} على موقعه الإلكتروني كما يلي حيث بلغ &
", Verb_Article_Bank_1(), bank.Name, Analytics.Day(), Analytics.Date());

                string summary = string.Format(@"{0} أسعار العملات في {1} صباح اليوم {2} الموافق {3}
وباقي أسعار العملات في مختلف البنوك التي تعمل في السوق المصري ذلك بمقارنة أسعارالعملات المعلنة على موقع {1} وباقي البنوك
", Verb_Article_Bank_1(), bank.Name, Analytics.Day(), Analytics.Date());

                string _rate = string.Empty;
                foreach (var item in db.Rates.Include("Bank").Include("Currency").Where(r => r.BankId == bank.Id).OrderBy(r => r.CurrencyId))
                {
                    _rate += "سعر شراء " + item.Currency.Name + " في " + item.Bank.Name + " " + item.SalePrice.ToString() + " وسعر بيع " + item.Currency.Name + " في " + item.Bank.Name + " " + item.BuyPrice.ToString() + "," + "&";
                }

                content += _rate;

                int i = content.Length;
                content = content.Remove(i - 3);
                content += ".";

                Article article = new Article
                {
                    Title = string.Format("أسعار العملات في {0} صباح اليوم {1} الموافق {2}", bank.Name, Analytics.Day(), Analytics.Date()),
                    Content = content,
                    Description = string.Format("تابع أسعار العملات في {0} اليوم {1} {2}", bank.Name, Analytics.Day(), Analytics.Date()),
                    Keywords = string.Format("{0}, أسعار العملات في {0} , العملات في {0}", bank.Name),
                    Date = DateTime.Now,
                    Image = "bank_" + bank.Id + ".jpg",
                    TypeId = 2,
                    Summary = summary,
                    BankId = bank.Id,
                    Ispermanent = false
                };

                db.Articles.Add(article);
            }
            db.SaveChanges();
        }

        public void Article_CurrenciesInBanks()
        {
            var db = new EgyptCurrencyRatesContext();

            List<Article> articles = new List<Article>();
            foreach (var Currency in db.Currencies)
            {
                foreach (var bank in db.Banks.Where(b => b.Visible == true))
                {
                    int bankId = bank.Id;
                    int CurrencyId = Currency.Id;


                    Rate rate = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == bankId).FirstOrDefault();
                    if (rate == null)
                        continue;


                    string content = string.Format(@"سجل سعر {0} في {1} {2} {3} شراء {4} وبيع {5}{6}", rate.Currency.Name,
                        rate.Bank.Name, Analytics.Day(), "15-7-2012", rate.BuyPrice, rate.SalePrice, Environment.NewLine);


                    Rate Buy_Price = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == bankId).OrderByDescending(r => r.BuyPrice).FirstOrDefault();
                    Rate Sale_Price = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == bankId).OrderBy(r => r.SalePrice).FirstOrDefault();


                    string content2 = string.Format("كما أن  سعر {0} في {1} هو أعلى سعر شراء مقابل {2} وان سعر {0} في {3} هو أقل سعر بيع مقابل {4}{5}",
                        rate.Currency.Name, Buy_Price.Bank.Name, Buy_Price.BuyPrice, Sale_Price.Bank.Name, Sale_Price.SalePrice, Environment.NewLine);


                    Rate Central_Bank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == CurrencyId && r.BankId == 8).FirstOrDefault();
                    if (Central_Bank == null)
                        continue;


                    string content3 = string.Format("يذكر أن  سعر شراء {0} في البنك المركزي المصري هو {1} وان سعر بيع {0} في البنك المركزي المصري هو {2}{3}",
                       Central_Bank.Currency.Name, Central_Bank.BuyPrice, Central_Bank.SalePrice, Environment.NewLine);

                    string Summary = content + content2;

                    Article article2 = new Article()
                    {
                        Title = string.Format(@"سعر {0} في {1} اليوم {2} {3}", rate.Currency.Name, rate.Bank.Name, Analytics.Day(), Analytics.Date()),
                        Content = content + "&" + content2 + "&" + content3,
                        Summary = Summary,
                        Description = string.Format(@"سعر {0} في {1} اليوم", rate.Currency.Name, rate.Bank.Name),
                        Keywords = string.Format(@"سعر {0} في {1} اليوم , سعر {0} , {0}", rate.Currency.Name, rate.Bank.Name),
                        Date = DateTime.Now,
                        Image = "currency_" + CurrencyId + ".jpg",
                        TypeId = 3,
                        BankId = bankId,
                        CurrencyId = CurrencyId,
                        Ispermanent = false
                    };
                    articles.Add(article2);
                }
            }
            db.Articles.AddRange(articles);
            db.SaveChanges();
        }

        public string Verb_Article_Bank_1()
        {
            string[] verb = { "تفاوتت مستويات", "تباينت مستويات", "اختلفت  مستويات", "تفاوتت", "تباينت", "اختلفت" };

            Random random = new Random();
            var randomIndex = random.Next(0, verb.Length);
            return verb[randomIndex];
        }

        public void Article_Buy()
        {
            var db = new EgyptCurrencyRatesContext();

            var Currencies = db.Currencies.Where(c => c.Visiable == true);
            var Banks = db.Banks.Where(b => b.Visible == true);

            foreach (var currency in Currencies)
            {
                foreach (var bank in Banks)
                {
                    try
                    {
                        var rate = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId == bank.Id).FirstOrDefault();
                        if (rate is null)
                        {
                            continue;
                        }

                        string content = string.Format("سجل سعر شراء {0} في {1} اليوم {2} {3} للشراء {4} . {5}", currency.Name, rate.Bank.Name, Analytics.Day(), Analytics.Date(), rate.BuyPrice, Environment.NewLine);


                        var BuyPrice = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId != 8 && r.BankId != 9).OrderByDescending(r => r.BuyPrice);
                        Rate High_Buy_Price_Rate = BuyPrice.FirstOrDefault();
                        double High_Buy_Price = High_Buy_Price_Rate.BuyPrice;
                        string High_Buy_Bank_Name = High_Buy_Price_Rate.Bank.Name;

                        string summary = content;

                        content += content = string.Format("وكان أعلى سعر لشراء {0} في جميع البنوك العاملة في السوق المصري سجله {1} والذي بلغ سعر الشراء فيه {2} جنيه مصري مقابل {0} الواحد {3}.",
                            currency.Name, High_Buy_Bank_Name, High_Buy_Price, Environment.NewLine);


                        Rate Central_Bank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId == 8).FirstOrDefault();

                        content += content = string.Format("علما بأن سعر شراء {0} في البنك المركزي المصري {1} وهو سعر استرشادي لبقية البنوك.", currency.Name, Central_Bank.BuyPrice);




                        string description = string.Format("سعر شراء {0} في {1} اليوم وأعلى سعر لشراء {0} في السوق المصري بتاريخ اليوم", currency.Name, rate.Bank.Name);

                        string keywords = string.Format("سعر {0}, سعر شراء {0}, {1}, أعلى سعر لشراء {0} , سعر {0} اليوم , سعر {0},سعر {0} في {1} اليوم ,سعر شراء {0} في {1} اليوم", currency.Name, rate.Bank.Name);
                        //{0}
                        Article article = new Article()
                        {
                            Title = string.Format("سعر شراء {0} في {1} اليوم {2} {3}", currency.Name, rate.Bank.Name, Analytics.Day(), Analytics.Date()),
                            Content = content,
                            Description = description,
                            Summary = summary,
                            Keywords = keywords,
                            TypeId = 4,
                            Date = Analytics.DateTime(),
                            CurrencyId = currency.Id,
                            BankId = rate.BankId,
                            Image = "currency_" + currency.Id + ".jpg",
                            Ispermanent = false
                        };
                        db.Articles.Add(article);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            db.SaveChanges();
        }

        public void Article_Sale()
        {
            var db = new EgyptCurrencyRatesContext();

            var Currencies = db.Currencies.Where(c => c.Visiable == true);
            var Banks = db.Banks.Where(b => b.Visible == true);

            foreach (var currency in Currencies)
            {
                foreach (var bank in Banks)
                {
                    try
                    {
                        var rate = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId == bank.Id).FirstOrDefault();
                        if (rate is null)
                        {
                            continue;
                        }

                        string content = string.Format("سجل سعر بيع {0} في {1} اليوم {2} {3} للبيع {4} . {5}", currency.Name, rate.Bank.Name, Analytics.Day(), Analytics.Date(), rate.SalePrice, Environment.NewLine);

                        var SalePrice = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId != 8 && r.BankId != 9).OrderByDescending(r => r.SalePrice);

                        Rate Law_Sale_Price_Rate = SalePrice.OrderBy(r => r.SalePrice).FirstOrDefault();
                        double Law_Sale_Price = Law_Sale_Price_Rate.SalePrice;
                        string Law_Sale_Price_name = Law_Sale_Price_Rate.Bank.Name;

                        string summary = content;

                        content += content = string.Format("وكان أقل سعر لبيع {0} في جميع البنوك العاملة في السوق المصري سجله {1} والذي بلغ سعر البيع فيه {2} جنيه مصري مقابل {0} الواحد {3}.",
                            currency.Name, Law_Sale_Price_name, Law_Sale_Price, Environment.NewLine);


                        Rate Central_Bank = db.Rates.Include("Bank").Include("Currency").Where(r => r.CurrencyId == currency.Id && r.BankId == 8).FirstOrDefault();

                        content += content = string.Format("علما بأن سعر بيع {0} في البنك المركزي المصري {1} وهو سعر استرشادي لبقية البنوك.", currency.Name, Central_Bank.SalePrice);




                        string description = string.Format("سعر بيع {0} في {1} اليوم وأقل سعر لبيع {0} في السوق المصري بتاريخ اليوم", currency.Name, rate.Bank.Name);

                        string keywords = string.Format("سعر {0}, سعر بيع {0}, {1}, أقل سعر لبيع {0} , سعر {0} اليوم , سعر {0},سعر {0} في {1} اليوم ,سعر بيع {0} في {1} اليوم", currency.Name, rate.Bank.Name);
                        //{0}
                        Article article = new Article()
                        {
                            Title = string.Format("سعر بيع {0} في {1} اليوم {2} {3}", currency.Name, rate.Bank.Name, Analytics.Day(), Analytics.Date()),
                            Content = content,
                            Description = description,
                            Summary = summary,
                            Keywords = keywords,
                            TypeId = 5,
                            Date = Analytics.DateTime(),
                            CurrencyId = currency.Id,
                            BankId = rate.BankId,
                            Image = "currency_" + currency.Id + ".jpg",
                            Ispermanent = false
                        };
                        db.Articles.Add(article);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            db.SaveChanges();
        }
    }
}