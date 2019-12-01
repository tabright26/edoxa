// Filename: ClanMemberRemovedDomainEvent.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.DomainEvents
{
    public sealed class ClanMemberRemovedDomainEvent : IDomainEvent
    {
        public ClanMemberRemovedDomainEvent(UserId userId, ClanId clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

        public UserId UserId { get; }

        public ClanId ClanId { get; }
    }
}
