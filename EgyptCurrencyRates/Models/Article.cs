using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Article
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? Keywords { get; set; }

    public DateTime? Date { get; set; }

    public string? Image { get; set; }

    public int? TypeId { get; set; }

    public int? CurrencyId { get; set; }

    public int? BankId { get; set; }

    public bool? Ispermanent { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual ArticleType? Type { get; set; }
}
