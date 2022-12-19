using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class InputSheet
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public int? BrandId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

}
