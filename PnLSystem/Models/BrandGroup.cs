using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class BrandGroup
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? GroupId { get; set; }

    public bool IsAdmin { get; set; }

    public virtual Brand Group { get; set; }

    public virtual User User { get; set; }
}
