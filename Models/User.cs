using System;
using System.Collections.Generic;

namespace Razor_PagesMovie.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateTime? UserBegindate { get; set; }

    public DateTime? UserEnddate { get; set; }

    public string? UserEmail { get; set; }

    public string? UserRole { get; set; }

    public DateTime? UserBirthday { get; set; }

    public DateTime? Userctime { get; set; }
}
