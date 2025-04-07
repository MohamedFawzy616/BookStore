using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.ViewModels
{
    public class RateViewModel
    {
        public int Id { get; set; }

        public int BankId { get; set; }
        public string Bank { get; set; }
        public string BankUrl { get; set; }


        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }


        public double Buy_Price { get; set; }
        public double Sale_Price { get; set; }

        public double Transfer_Buy { get; set; }
        public double Transfer_Sale { get; set; }

        public string BankLogo { get; set; }
        public string CurrencyLogo { get; set; }
        
        public System.DateTime Date { get; set; }
    }
}
