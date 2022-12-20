using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class PnLreport
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public int? BrandId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public double? TotalLost { get; set; }

    public double? TotalProfit { get; set; }


}
