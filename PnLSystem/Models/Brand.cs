using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<BrandGroup> BrandGroups { get; } = new List<BrandGroup>();

    public virtual ICollection<Category> Categories { get; } = new List<Category>();

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual ICollection<InputSheet> InputSheets { get; } = new List<InputSheet>();

    public virtual ICollection<PnLreport> PnLreports { get; } = new List<PnLreport>();

    public virtual ICollection<StoreGroup> StoreGroups { get; } = new List<StoreGroup>();

    public virtual ICollection<Store> Stores { get; } = new List<Store>();
}
