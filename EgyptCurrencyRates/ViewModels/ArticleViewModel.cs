using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptCurrencyRates.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public DateTime? Date { get; set; }
        public string Image { get; set; }
        public int? TypeId { get; set; }
        public int? CurrencyId { get; set; }
        public int? BankId { get; set; }
        public bool Article2 { get; set; }
        public bool? Ispermanent { get; set; }
        //public Bank Bank { get; set; }
        //public Currency Currency { get; set; }
        //public ArticleType Type { get; set; }
    }
}
