using EgyptCurrencyRates.ViewModels;
using Newtonsoft.Json;

namespace EgyptCurrencyRates.Help
{
    public class FileModels
    {
        IWebHostEnvironment hosting;
        public FileModels(IWebHostEnvironment _hosting)
        {
            hosting = _hosting;
        }

        public CounterViewModel LoadCounter()
        {
            return JsonConvert.DeserializeObject<CounterViewModel>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Posts/counter.txt")));
        }

        public List<PostViewModel> Body()
        {
            return JsonConvert.DeserializeObject<List<PostViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Posts/Body.txt")));
        }
        public List<PostViewModel> Footer()
        {
            return JsonConvert.DeserializeObject<List<PostViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Posts/Footer.txt")));
        }
        public List<PostViewModel> Header()
        {
            return JsonConvert.DeserializeObject<List<PostViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Posts/Header.txt")));
        }
        public List<RateViewModel> RatesByBankId(int bankId)
        {
            return JsonConvert.DeserializeObject<List<RateViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Banks/" + bankId + "_Rates.txt")));
        }


        public List<RateViewModel> RatesByCurrencyId(int currencyId)
        {
            return JsonConvert.DeserializeObject<List<RateViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Currencies/" + currencyId + "_Rates.txt")));
        }


        public List<BankViewModel> Banks()
        {
            return JsonConvert.DeserializeObject<List<BankViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Banks.txt")));
        }


        public List<CurrencyViewModel> Currencies()
        {
            return JsonConvert.DeserializeObject<List<CurrencyViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Currencies.txt")));
        }


        public List<RateViewModel> Rates()
        {
            return JsonConvert.DeserializeObject<List<RateViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Rates.txt")));
        }

        public List<GoldPriceModel> GoldPrices()
        {
            return JsonConvert.DeserializeObject<List<GoldPriceModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/GoldPrices.txt")));
        }

        //public List<RateViewModel> Rates(int bankId)
        //{
        //    return JsonConvert.DeserializeObject<List<RateViewModel>>(System.IO.File.ReadAllText(Path.Combine(hosting.WebRootPath, "JsonFiles/Rates.txt")));
        //}
    }
}
