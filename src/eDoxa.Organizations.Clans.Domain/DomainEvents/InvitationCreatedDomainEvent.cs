// Filename: InvitationCreatedDomainEvent.cs
// Date Created: 2019-10-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.DomainEvents
{
    public sealed class InvitationCreatedDomainEvent : IDomainEvent
    {
        public InvitationCreatedDomainEvent(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; }
    }
}
