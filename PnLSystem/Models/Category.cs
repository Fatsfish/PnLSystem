using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Category
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsDelete { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
