using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDelete { get; set; }

    public string Status { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual ICollection<InputSheet> InputSheets { get; } = new List<InputSheet>();

    public virtual ICollection<PnLreport> PnLreports { get; } = new List<PnLreport>();

    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();
}
