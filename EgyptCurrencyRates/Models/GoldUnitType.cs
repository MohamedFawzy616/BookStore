using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class GoldUnitType
{
    public int Id { get; set; }

    public string? TitleAr { get; set; }

    public string? TitleEn { get; set; }

    public virtual ICollection<GoldUnit> GoldUnits { get; set; } = new List<GoldUnit>();
}
