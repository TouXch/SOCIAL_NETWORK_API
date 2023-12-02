using System;
using System.Collections.Generic;

namespace SOCIAL_NETWORK_API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;
}
