using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class ReportRevenue
{
    public int Id { get; set; }

    public int? SheetId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public double? Value { get; set; }

    public virtual PnLreport Sheet { get; set; }
}
