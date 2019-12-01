// Filename: InvitationAcceptedDomainEvent.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.DomainEvents
{
    public sealed class InvitationAcceptedDomainEvent : IDomainEvent
    {
        public InvitationAcceptedDomainEvent(Invitation invitation)
        {
            ClanId = invitation.ClanId;
            Invitation = invitation;
        }

        public ClanId ClanId { get; }

        public IMemberInfo Invitation { get; }
    }
}
