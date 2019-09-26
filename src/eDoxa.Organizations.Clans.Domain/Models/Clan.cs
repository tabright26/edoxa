// Filename: ClanModel.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class Clan : Entity<ClanId>
    {
        public Clan(string name, string summary, UserId ownerId)
        {
            Name = name;
            Summary = summary;
            OwnerId = ownerId;
            Members = new List<Member>();
        }

        public string Name { get; private set; }

        public string Summary { get; private set; }

        //TODO: Maybe use a owner Member instead. This would garantee that at least one member is alwyas present in a clan.
        public UserId OwnerId { get; private set; }

        public ICollection<Member> Members { get; }

        public void AddMember(IMemberInfo memberInfo)
        {
            var member = new Member(memberInfo);
            Members.Add(member);
        }
    }
}
