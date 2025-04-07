using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class GoldUnit
{
    public int Id { get; set; }

    public string TitleAr { get; set; } = null!;

    public string? TitleEn { get; set; }

    public int? GoldUnitType { get; set; }

    public virtual ICollection<GoldPriceLog> GoldPriceLogs { get; set; } = new List<GoldPriceLog>();

    public virtual ICollection<GoldPrice> GoldPrices { get; set; } = new List<GoldPrice>();

    public virtual GoldUnitType? GoldUnitTypeNavigation { get; set; }
}
