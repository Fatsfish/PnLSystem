using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class InputSheetExpenseImport
{
    public int Id { get; set; }

    public int? SheetId { get; set; }

    public int? StockId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public string ImageLink { get; set; }

    public double? Value { get; set; }

    public bool Type { get; set; }

    public bool IsFinished { get; set; }

}
