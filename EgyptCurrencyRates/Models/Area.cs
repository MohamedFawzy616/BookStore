using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Area
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? CityId { get; set; }

    public int? Order { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<ItemArea> ItemAreas { get; set; } = new List<ItemArea>();
}
