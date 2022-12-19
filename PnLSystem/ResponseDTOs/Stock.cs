using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class Stock
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageLink { get; set; }

    public double? Value { get; set; }

    public bool IsDelete { get; set; }

}
