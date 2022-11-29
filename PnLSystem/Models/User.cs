using System;
using System.Collections.Generic;

namespace PnLSystem.Models;

public partial class User
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BrandGroup> BrandGroups { get; } = new List<BrandGroup>();

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual ICollection<HelpRequest> HelpRequests { get; } = new List<HelpRequest>();

    public virtual ICollection<StoreGroup> StoreGroups { get; } = new List<StoreGroup>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
