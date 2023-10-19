using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string? Rolename { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
