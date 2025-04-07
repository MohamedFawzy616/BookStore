using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class ExceptionsLog
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public string? Exeption { get; set; }

    public DateTime? Time { get; set; }
}
