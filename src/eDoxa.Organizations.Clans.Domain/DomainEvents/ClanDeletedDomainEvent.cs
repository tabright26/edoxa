// Filename: ClanDeletedDomainEvent.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.DomainEvents
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
