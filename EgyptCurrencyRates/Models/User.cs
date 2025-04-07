using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Ip { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Hostname { get; set; }

    public string? Loc { get; set; }

    public string? Org { get; set; }

    public string? Region { get; set; }

    public bool? BookMark { get; set; }
}
