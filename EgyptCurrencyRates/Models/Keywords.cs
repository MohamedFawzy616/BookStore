using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models
{
    public partial class Keywords
    {
        public Keywords()
        {
            ItemKeyword = new HashSet<ItemKeyword>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Order { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<ItemKeyword> ItemKeyword { get; set; }
    }
}
