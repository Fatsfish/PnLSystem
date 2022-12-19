using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class StoreGroup
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? GroupId { get; set; }

    public bool IsAdmin { get; set; }
}
