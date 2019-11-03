// Filename: ClanMemberAddedDomainEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Clans.Domain.DomainEvents
{
    public sealed class ClanMemberAddedDomainEvent : IDomainEvent
    {
        public ClanMemberAddedDomainEvent(UserId userId, ClanId clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

        public UserId UserId { get; }

        public ClanId ClanId { get; }
    }
}
