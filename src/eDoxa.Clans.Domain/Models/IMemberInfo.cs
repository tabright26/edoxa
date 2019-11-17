// Filename: IMemberInfo.cs
// Date Created: --
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Clans.Domain.Models
{
    public interface IMemberInfo
    {
        UserId UserId { get; }

        ClanId ClanId { get; }
    }
}
