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
        public Member(IMemberInfo memberInfo) : this(memberInfo.ClanId, memberInfo.UserId)
        {
        }

        public Member(ClanId clanId, UserId userId) : this()
        {
            ClanId = clanId;
            UserId = userId;
        }

        private Member()
        {
            // Required By EF CORE
        }

        public UserId UserId { get; private set; }

        public ClanId ClanId { get; private set; }
    }
}
