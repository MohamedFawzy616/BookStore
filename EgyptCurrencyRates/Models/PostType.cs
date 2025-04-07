using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class PostType
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
