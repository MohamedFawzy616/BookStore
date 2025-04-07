using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Group
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? IdNumber { get; set; }

    public virtual ICollection<GroupTypeAccount> GroupTypeAccounts { get; set; } = new List<GroupTypeAccount>();

    public virtual ICollection<PostGroup> PostGroups { get; set; } = new List<PostGroup>();
}
