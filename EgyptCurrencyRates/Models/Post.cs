using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? TypeId { get; set; }

    public virtual ICollection<PostGroup> PostGroups { get; set; } = new List<PostGroup>();

    public virtual PostType? Type { get; set; }
}
