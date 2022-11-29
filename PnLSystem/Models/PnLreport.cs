using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

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

    public virtual Brand Brand { get; set; }

    public virtual ICollection<ReportExpense> ReportExpenses { get; } = new List<ReportExpense>();

    public virtual ICollection<ReportRevenue> ReportRevenues { get; } = new List<ReportRevenue>();

    public virtual Store Store { get; set; }
}
