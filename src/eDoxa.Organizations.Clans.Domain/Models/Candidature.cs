// Filename: ClanModel.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class Candidature : Entity<CandidatureId>, IMemberInfo
    {
        public Candidature(UserId userId, ClanId clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

        public UserId UserId { get; private set; }

        public ClanId ClanId { get; private set; }
    }
}
