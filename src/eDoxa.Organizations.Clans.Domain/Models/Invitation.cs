// Filename: Invitation.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public sealed class Invitation : Entity<InvitationId>, IMemberInfo
    {
        public Invitation(UserId userId, ClanId clanId) : this()
        {
            UserId = userId;
            ClanId = clanId;
        }
#nullable disable
        private Invitation()
        {
            //Requied by EF Core
        }
#nullable restore
        public UserId UserId { get; private set; }

        public ClanId ClanId { get; private set; }

        public void Accept()
        {
            this.AddDomainEvent(new InvitationAcceptedDomainEvent(this));
        }
    }
}
