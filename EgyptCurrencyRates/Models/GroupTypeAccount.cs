using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class GroupTypeAccount
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public int? AccountId { get; set; }

    public int? TypeId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Group? Group { get; set; }

    public virtual GroupType? Type { get; set; }
}
