using System;
using System.Collections.Generic;

namespace EgyptCurrencyRates.Models;

public partial class Rate
{
    public int Id { get; set; }

    public int CurrencyId { get; set; }

    public int BankId { get; set; }

    public double BuyPrice { get; set; }

    public double SalePrice { get; set; }

    public double TransferBuy { get; set; }

    public double TransferSale { get; set; }

    public DateTime Date { get; set; }

    public string? BuyArrow { get; set; }

    public string? SaleArrow { get; set; }

    public virtual Bank Bank { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;
}
