using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models
{
    public partial class Errors
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}
