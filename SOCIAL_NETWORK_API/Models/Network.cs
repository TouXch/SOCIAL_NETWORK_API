using System;
using System.Collections.Generic;

namespace SOCIAL_NETWORK_API.Models;

public partial class Network
{
    public int FriendshipId { get; set; }

    public int User1Id { get; set; }

    public int User2Id { get; set; }

    public string RelationType { get; set; } = "friendship";
}
