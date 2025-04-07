using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.ViewModels
{
    public class GoldPriceModel
    {
        public int Id { get; set; }
        public string EgyptianPound { get; set; }
        public string USADollar { get; set; }
        public DateTime? Date { get; set; }


        public int? GoldUnitId { get; set; }
        public string GoldUnitTitleAr { get; set; }
        public string GoldUnitTitleEn { get; set; }



        public int UnitTypeId { get; set; }
        public string UnitTypeTitleAr { get; set; }
        public string UnitTypeTitleEn { get; set; }
    }
}