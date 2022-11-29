using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
