// Filename: ClanModel.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class Invitation : Entity<InvitationId>, IMemberInfo
    {
        public Invitation(UserId userId, ClanId clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

        public UserId UserId { get; set; }

        public ClanId ClanId { get; set; }
    }
}
