using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class GoldPriceLog
{
    public int Id { get; set; }

    public int? GoldUnit { get; set; }

    public string? EgyptianPound { get; set; }

    public string? UsaDollar { get; set; }

    public DateTime? Date { get; set; }

    public virtual GoldUnit? GoldUnitNavigation { get; set; }
}
