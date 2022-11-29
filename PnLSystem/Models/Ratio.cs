using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Ratio
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? StockId { get; set; }

    public DateTime CreationDate { get; set; }

    public double? Value { get; set; }

    public bool IsActive { get; set; }

    public virtual Product Product { get; set; }

    public virtual Stock Stock { get; set; }
}
