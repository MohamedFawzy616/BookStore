using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class GroupType
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<GroupTypeAccount> GroupTypeAccounts { get; set; } = new List<GroupTypeAccount>();
}
