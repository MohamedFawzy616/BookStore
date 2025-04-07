using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.ViewModels
{
    public class CurrencyViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Visiable { get; set; }
        public int? Order { get; set; }
        public string Logo { get; set; }
    }
}
