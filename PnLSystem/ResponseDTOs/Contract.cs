using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class Contract
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BrandId { get; set; }

    public int? StoreId { get; set; }

    public DateTime CreationDate { get; set; }

    public string ImageLink { get; set; }

    public double? Value { get; set; }

    public bool IsActive { get; set; }

}
