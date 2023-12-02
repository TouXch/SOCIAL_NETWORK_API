using System;
using System.Collections.Generic;

namespace SOCIAL_NETWORK_API.Models;

public partial class PostLiked
{
    public int Id { get; set; }

    public int UserLike { get; set; }

    public int PostLiked1 { get; set; }

    public bool LikeStatus { get; set; }
}
