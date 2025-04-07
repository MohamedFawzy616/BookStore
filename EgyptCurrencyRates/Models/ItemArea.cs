using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class ItemArea
{
    public int Id { get; set; }

    public int? ItemId { get; set; }

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual Item? Item { get; set; }
}
