using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class ArticleType
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
