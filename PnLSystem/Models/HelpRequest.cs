using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class HelpRequest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public int? CreationUserId { get; set; }

    public bool IsDelete { get; set; }

    public string Status { get; set; }

    public virtual User CreationUser { get; set; }
}
