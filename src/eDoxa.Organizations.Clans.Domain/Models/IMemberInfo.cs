// Filename: IMemberInfo.cs
// Date Created: --
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public interface IMemberInfo
    {
        UserId UserId { get; }

        ClanId ClanId { get; }
    }
}
