using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageLink { get; set; }

    public bool IsDelete { get; set; }

}
