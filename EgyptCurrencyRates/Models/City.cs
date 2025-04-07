using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class City
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Order { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
