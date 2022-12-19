using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class Category
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsDelete { get; set; }

}
