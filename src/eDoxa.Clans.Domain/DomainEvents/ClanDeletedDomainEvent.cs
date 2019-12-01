// Filename: ClanDeletedDomainEvent.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.DomainEvents
{
    public sealed class ClanDeletedDomainEvent : IDomainEvent
    {
        public ClanDeletedDomainEvent(ClanId clanId)
        {
            ClanId = clanId;
        }

        public ClanId ClanId { get; }
    }
}
