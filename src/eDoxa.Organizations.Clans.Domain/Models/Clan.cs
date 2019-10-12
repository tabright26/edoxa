// Filename: Clan.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Organizations.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public sealed class Clan : Entity<ClanId>
    {
        public Clan(string name, UserId ownerId) : this()
        {
            Name = name;
            Summary = null;
            OwnerId = ownerId;
            Members = new HashSet<Member>();
            this.AddMember(new Member(Id, ownerId));
        }
#nullable disable
        private Clan()
        {
            //Requied by EF Core
        }
#nullable restore
        public string Name { get; private set; }

        public string? Summary { get; private set; }

        public UserId OwnerId { get; private set; }

        public ICollection<Member> Members { get; private set; }

        public void AddMember(IMemberInfo memberInfo)
        {
            if (this.HasMember(memberInfo.UserId))
            {
                throw new InvalidOperationException();
            }

            var member = new Member(memberInfo);

            Members.Add(member);

            this.AddDomainEvent(new ClanMemberAddedDomainEvent(member.UserId));
        }

        public void Leave(Member member)
        {
            this.Remove(member);

            if (this.MemberIsOwner(member))
            {
                if (this.CanDelegateOwnership())
                {
                    this.DelegateOwnership(Members.First());
                }
                else
                {
                    this.AddDomainEvent(new ClanDeletedDomainEvent(Id));    
                }
            }
        }

        public void Kick(Member member)
        {
            if (this.MemberIsOwner(member))
            {
                throw new InvalidOperationException();
            }

            this.Remove(member);
        }

        private void Remove(Member member)
        {
            Members.Remove(member);
        }

        private bool MemberIsOwner(Member member)
        {
            return this.MemberIsOwner(member.UserId);
        }

        public bool MemberIsOwner(UserId userId)
        {
            return OwnerId == userId;
        }

        public bool CanDelegateOwnership()
        {
            return Members.Count > 1;
        }

        public bool IsDelete()
        {
            return DomainEvents.Any(domainEvent => domainEvent is ClanDeletedDomainEvent);
        }

        public bool HasMember(UserId userId)
        {
            return Members.Any(member => member.UserId == userId);
        }

        public bool HasMember(MemberId memberId)
        {
            return Members.Any(member => member.Id == memberId);
        }

        public Member FindMember(UserId userId)
        {
            return Members.Single(member => member.UserId == userId);
        }

        public Member FindMember(MemberId memberId)
        {
            return Members.Single(member => member.Id == memberId);
        }

        private void DelegateOwnership(Member member)
        {
            OwnerId = member.UserId;
        }
    }
}
