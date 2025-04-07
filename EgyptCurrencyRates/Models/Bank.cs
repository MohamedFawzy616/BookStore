using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Bank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Logo { get; set; }

    public bool? Visible { get; set; }

    public int? Order { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<RateLog> RateLogs { get; set; } = new List<RateLog>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
