using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Account
{
    public int Accountid { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public string? Fullname { get; set; }

    public int? Roleid { get; set; }

    public DateTime? Lastlogin { get; set; }

    public DateTime? Createdate { get; set; }

    public bool? Active { get; set; }

    public virtual Role? Role { get; set; }
}
