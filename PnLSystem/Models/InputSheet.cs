using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class InputSheet
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public int? BrandId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<InputSheetExpense> InputSheetExpenses { get; } = new List<InputSheetExpense>();

    public virtual ICollection<InputSheetRevenue> InputSheetRevenues { get; } = new List<InputSheetRevenue>();

    public virtual Store Store { get; set; }
}
