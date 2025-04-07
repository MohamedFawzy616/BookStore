using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Newtonsoft.Json;

namespace EgyptCurrencyRates.Help
{
    [ExceptionLog]
    public class FileCreator
    {
        IWebHostEnvironment hosting;

        public FileCreator(IWebHostEnvironment _hosting)
        {
            hosting = _hosting;
        }


        public void SaveCounter(CounterViewModel models)
        {
            string json = JsonConvert.SerializeObject(models);
            string path = Path.Combine(hosting.WebRootPath, "JsonFiles/Posts/counter.txt");
            System.IO.File.WriteAllText(path, json);
        }


        public void Banks()
        {
            var db = new EgyptCurrencyRatesContext();
            List<BankViewModel> models = db.Banks.Select(b => new BankViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order,
                Visible = b.Visible,
                Logo = b.Logo
            }).ToList();

            string json = JsonConvert.SerializeObject(models);
            string path =  Path.Combine(hosting.WebRootPath, "JsonFiles/Banks.txt");
            System.IO.File.WriteAllText(path, json);
        }

        public void Currencies()
        {
            var db = new EgyptCurrencyRatesContext();
            List<CurrencyViewModel> models = db.Currencies.Select(c => new CurrencyViewModel
            {
                ID = c.Id,
                Name = c.Name,
                Code = c.Code,
                Visiable = c.Visiable,
                Order = c.Order,
                Logo = c.Logo
            }).ToList();
            string json = JsonConvert.SerializeObject(models);
            string path = Path.Combine(hosting.WebRootPath, "JsonFiles/Currencies.txt");
            System.IO.File.WriteAllText(path, json);
        }

        public void CreateGoldFiles()
        {
            var db = new EgyptCurrencyRatesContext();
            List<GoldPriceModel> goldPrices = db.GoldPrices.Select(g => new GoldPriceModel
            {
                Id = g.Id,

                EgyptianPound = g.EgyptianPound,
                USADollar = g.UsaDollar,
                Date = g.Date,

                GoldUnitId = g.GoldUnit,
                GoldUnitTitleAr = g.GoldUnitNavigation.TitleAr,
                GoldUnitTitleEn = g.GoldUnitNavigation.TitleEn,

                UnitTypeId = g.GoldUnitNavigation.GoldUnitTypeNavigation.Id,
                UnitTypeTitleAr = g.GoldUnitNavigation.GoldUnitTypeNavigation.TitleAr,
                UnitTypeTitleEn = g.GoldUnitNavigation.GoldUnitTypeNavigation.TitleEn


            }).ToList();

            string goldPricesjson = JsonConvert.SerializeObject(goldPrices);


            string path = Path.Combine(hosting.WebRootPath, "JsonFiles/GoldPrices.txt");
            System.IO.File.WriteAllText(path, goldPricesjson);

            //string goldPricesPath = @"C:\inetpub\wwwroot\EgyptCurrencyRates\wwwroot\JsonFiles\GoldPrices.txt";

            //System.IO.File.WriteAllText(goldPricesPath, goldPricesjson);


            List<GoldUnitTypeModel> goldUnitTypes = db.GoldUnitTypes.Select(t => new GoldUnitTypeModel
            {
                UnitTypeId = t.Id,
                UnitTypeTitleAr = t.TitleAr,
                UnitTypeTitleEn = t.TitleEn
            }).ToList();

            string goldUnitTypesjson = JsonConvert.SerializeObject(goldUnitTypes);
            string goldUnitTypesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/GoldUnitTypes.txt");
            System.IO.File.WriteAllText(goldUnitTypesPath, goldUnitTypesjson);
        }

        public void CreateBankRatesFile(int bankId)
        {
            var db = new EgyptCurrencyRatesContext();
            List<RateViewModel> Rates = db.Rates.Where(b => b.BankId == bankId).Select(r => new RateViewModel
            {
                Id = r.Id,
                BankId = r.BankId,
                Bank = r.Bank.Name,
                CurrencyId = r.CurrencyId,
                Currency = r.Currency.Name,
                Buy_Price = r.BuyPrice,
                Sale_Price = r.SalePrice,
                Date = r.Date,
                Transfer_Buy = r.TransferBuy,
                Transfer_Sale = r.TransferSale,
                BankLogo = r.Bank.Logo,
                CurrencyLogo = r.Currency.Logo,
            }).ToList();


            string Ratesjson = JsonConvert.SerializeObject(Rates);
            string RatesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/Banks/" + bankId.ToString() + "_Rates.txt");
            if (!File.Exists(RatesPath))
            {
                File.Create(RatesPath).Close();
            }
            System.IO.File.WriteAllText(RatesPath, Ratesjson);
        }

        public void CreateCurrenciesRatesFiles()
        {
            var db = new EgyptCurrencyRatesContext();
            List<RateViewModel> Rates = db.Rates.Select(r => new RateViewModel
            {
                Id = r.Id,
                BankId = r.BankId,
                Bank = r.Bank.Name,
                CurrencyId = r.CurrencyId,
                Currency = r.Currency.Name,
                Buy_Price = r.BuyPrice,
                Sale_Price = r.SalePrice,
                Date = r.Date,
                Transfer_Buy = r.TransferBuy,
                Transfer_Sale = r.TransferSale,
                BankLogo = r.Bank.Logo,
                CurrencyLogo = r.Currency.Logo,
            }).ToList();

            var GroupedRatesByCurrencyId = Rates.GroupBy(r => new { r.CurrencyId })
    .Select(rate => new
    {
        Currency = rate.Key.CurrencyId,
        rates = rate
    }).ToList();


            foreach (var item in GroupedRatesByCurrencyId)
            {
                string Ratesjson = JsonConvert.SerializeObject(item.rates);
                string RatesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/Currencies/" + item.Currency.ToString() + "_Rates.txt");
                if (!File.Exists(RatesPath))
                {
                    File.Create(RatesPath).Close();
                }
                System.IO.File.WriteAllText(RatesPath, Ratesjson);
            }
        }

        public void CreateRatesFiles()
        {
            var db = new EgyptCurrencyRatesContext();

            List<RateViewModel> Rates = db.Rates.Select(r => new RateViewModel
            {
                Id = r.Id,
                BankId = r.BankId,
                Bank = r.Bank.Name,
                CurrencyId = r.CurrencyId,
                Currency = r.Currency.Name,
                Buy_Price = r.BuyPrice,
                Sale_Price = r.SalePrice,
                Date = r.Date,
                Transfer_Buy = r.TransferBuy,
                Transfer_Sale = r.TransferSale,
                BankLogo = r.Bank.Logo,
                CurrencyLogo = r.Currency.Logo,
            }).ToList();


            var GroupedRatesByBankId = Rates.GroupBy(r => new { r.BankId })
                .Select(rate => new
                {
                    Bank = rate.Key.BankId,
                    rates = rate
                }).ToList();


            foreach (var item in GroupedRatesByBankId)
            {
                string Ratesjson = JsonConvert.SerializeObject(item.rates);
                string RatesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/Banks/" + item.Bank.ToString() + "_Rates.txt");
                if (!File.Exists(RatesPath))
                {
                    File.Create(RatesPath).Close();
                }
                System.IO.File.WriteAllText(RatesPath, Ratesjson);
            }


            var GroupedRatesByCurrencyId = Rates.GroupBy(r => new { r.CurrencyId })
                .Select(rate => new
                {
                    Currency = rate.Key.CurrencyId,
                    rates = rate
                }).ToList();


            foreach (var item in GroupedRatesByCurrencyId)
            {
                string Ratesjson = JsonConvert.SerializeObject(item.rates);
                string RatesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/Currencies/" + item.Currency.ToString() + "_Rates.txt");
                if (!File.Exists(RatesPath))
                {
                    File.Create(RatesPath).Close();
                }
                System.IO.File.WriteAllText(RatesPath, Ratesjson);
            }
        }

        //public void CreateGoldFiles()
        //{
        //    var db = new EgyptCurrencyRatesContext();

        //    var goldPricesFullData = db.GoldPrices.Select(g =>
        //    new
        //    {
        //        GoldPriceId = g.Id,
        //        EgyptianPound = g.EgyptianPound,
        //        UsaDollar = g.UsaDollar,
        //        Date = g.Date,
        //        GoldUnitId = g.GoldUnit,
        //        GoldUnitTitleAr = g.GoldUnitNavigation.TitleAr,
        //        GoldUnitTypeId = g.GoldUnitNavigation.GoldUnitType,
        //        GoldUnitType = g.GoldUnitNavigation.GoldUnitTypeNavigation.TitleAr
        //    });


        //    foreach (var item in goldPricesFullData)
        //    {
        //        string Ratesjson = JsonConvert.SerializeObject(item);
        //        string RatesPath = Path.Combine(hosting.WebRootPath, "JsonFiles/GoldPricesGoldRates.txt");
        //        if (!File.Exists(RatesPath))
        //        {
        //            File.Create(RatesPath).Close();
        //        }
        //        System.IO.File.WriteAllText(RatesPath, Ratesjson);
        //    }
        //}
    }
}
