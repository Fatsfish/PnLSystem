using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageLink { get; set; }

    public bool IsDelete { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<InputSheetRevenue> InputSheetRevenues { get; } = new List<InputSheetRevenue>();

    public virtual ICollection<Ratio> Ratios { get; } = new List<Ratio>();
}
