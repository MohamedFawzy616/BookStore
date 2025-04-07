using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<GroupTypeAccount> GroupTypeAccounts { get; set; } = new List<GroupTypeAccount>();
}
