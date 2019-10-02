// Filename: Invitation.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public sealed class Invitation : Entity<InvitationId>, IMemberInfo
    {
        public Invitation(UserId userId, ClanId clanId) : this()
        {
            UserId = userId;
            ClanId = clanId;
        }

        private Invitation()
        {
            //Requied by EF Core
        }

        public UserId UserId { get; private set; }

        public ClanId ClanId { get; private set; }

        public void Accept()
        {
            this.AddDomainEvent(new InvitationAcceptedDomainEvent(this));
        }
    }
}
