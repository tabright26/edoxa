// Filename: Member.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class Member : Entity<MemberId>
    {
        public Member(IMemberInfo memberInfo)
        {
            UserId = memberInfo.UserId;

            //Todo: Seriously, I dont think we need this ClanId, Member are already contained inside a clan and already has a clan foreing key.
            ClanId = memberInfo.ClanId;
        }

        public UserId UserId { get; private set; }

        public ClanId ClanId { get; private set; }
    }
}
