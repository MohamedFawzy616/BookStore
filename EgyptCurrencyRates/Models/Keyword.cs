using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Keyword
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Order { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ItemKeyword> ItemKeywords { get; set; } = new List<ItemKeyword>();
}
