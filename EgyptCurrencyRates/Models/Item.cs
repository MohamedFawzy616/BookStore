using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Address { get; set; }

    public string? Phones { get; set; }

    public string? Url { get; set; }

    public string? Map { get; set; }

    public string? Email { get; set; }

    public string? SwiftCode { get; set; }

    public string? Fax { get; set; }

    public int? TypeId { get; set; }

    public int? Order { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<ItemArea> ItemAreas { get; set; } = new List<ItemArea>();

    public virtual ICollection<ItemKeyword> ItemKeywords { get; set; } = new List<ItemKeyword>();

    public virtual Category? Type { get; set; }
}
