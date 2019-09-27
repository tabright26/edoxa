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
        public Clan(string name, UserId ownerId) : this()
        {
            Name = name;
            Summary = null;
            OwnerId = ownerId;
            Members = new List<Member>();
        }

        private Clan()
        {
            //Requied by EF Core
        }

        public string Name { get; private set; }

        public string Summary { get; private set; }

        //TODO: Maybe use a owner Member instead. This would garantee that at least one member is alwyas present in a clan.
        public UserId OwnerId { get; private set; }

        public ICollection<Member> Members { get; private set; }

        public void AddMember(IMemberInfo memberInfo)
        {
            var member = new Member(memberInfo);
            Members.Add(member);
        }

        public void ChangeOwner(Member member)
        {
            OwnerId = member.UserId;
        }

        public void ChangeSummary(string summary)
        {
            Summary = summary;
        }
    }
}
