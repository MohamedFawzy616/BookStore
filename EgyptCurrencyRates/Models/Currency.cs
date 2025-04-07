using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Currency
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? Visiable { get; set; }

    public int? Order { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<RateLog> RateLogs { get; set; } = new List<RateLog>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
