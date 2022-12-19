using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDelete { get; set; }

}
