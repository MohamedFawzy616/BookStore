using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? Order { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();
}
