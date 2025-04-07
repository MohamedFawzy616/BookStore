using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class ItemKeyword
{
    public int Id { get; set; }

    public int? ItemId { get; set; }

    public int? KeyWordId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Keyword? KeyWord { get; set; }
}
