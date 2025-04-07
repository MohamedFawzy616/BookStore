using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class PostGroup
{
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Post? Post { get; set; }
}
