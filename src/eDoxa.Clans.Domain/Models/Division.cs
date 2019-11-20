// Filename: Division.cs
// Date Created: 2019-10-31
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Clans.Domain.Models
{
    public class Division : Entity<DivisionId>
    {
        public Division(ClanId clanId, string name, string description) : this()
        {
            ClanId = clanId;
            Name = name;
            Description = description;
            Members = new HashSet<Member>();
        }
#nullable disable
        private Division()
        {
            //Requied by EF Core
        }
#nullable restore

        public ClanId ClanId { get; private set; }

        public ICollection<Member> Members { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool HasMember(MemberId memberId)
        {
            return Members.Any(member => member.Id == memberId);
        }

        public void AddMember(Member member)
        {
            if (this.HasMember(member.Id))
            {
                throw new InvalidOperationException();
            }

            Members.Add(member);
        }

        public void RemoveMember(Member member)
        {
            Members.Remove(member);
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
