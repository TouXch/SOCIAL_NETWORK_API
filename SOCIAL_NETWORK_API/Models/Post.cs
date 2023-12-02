using System;
using System.Collections.Generic;

namespace SOCIAL_NETWORK_API.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Text { get; set; } = null!;

    public string Visibility { get; set; } = null!;

    public DateTime PostedOn { get; set; }
}
