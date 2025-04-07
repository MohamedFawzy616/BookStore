using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.ViewModels
{
    public class BankViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Visible { get; set; }
        public int? Order { get; set; }
        public string Logo { get; set; }
    }
}
