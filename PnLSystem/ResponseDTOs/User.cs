using System;
using System.Collections.Generic;

namespace PnLSystem.ResponseDTOs;

public partial class User
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    public bool IsActive { get; set; }

}
