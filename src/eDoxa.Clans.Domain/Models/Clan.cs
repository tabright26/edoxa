// Filename: Clan.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.Models
{
    public sealed class Clan : Entity<ClanId>
    {
        public Clan(string name, UserId ownerId) : this()
        {
            Name = name;
            Summary = null;
            OwnerId = ownerId;
            Divisions = new HashSet<Division>();
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

        public bool Deleted => !Members.Any();

        public ICollection<Member> Members { get; private set; }

        public ICollection<Division> Divisions { get; private set; }

        public void UpdateDivision(DivisionId divisionId, string name, string description)
        {
            var division = Divisions.SingleOrDefault(div => div.Id == divisionId);

            if (division == null)
            {
                throw new InvalidOperationException();
            }

            division.Update(name, description);
        }

        public void AddMemberToDivision(DivisionId divisionId, MemberId memberId)
        {
            if (!this.HasMember(memberId))
            {
                throw new InvalidOperationException();
            }

            var memberNewDivision = Divisions.SingleOrDefault(division => division.Id == divisionId);

            if (memberNewDivision != null)
            {
                memberNewDivision.AddMember(this.FindMember(memberId));
            }
        }

        public void RemoveMemberFromDivision(DivisionId divisionId, MemberId memberId)
        {
            if (!this.HasMember(memberId))
            {
                throw new InvalidOperationException();
            }

            var memberCurrentDivision = Divisions.SingleOrDefault(division => division.Id == divisionId);

            if (memberCurrentDivision != null)
            {
                memberCurrentDivision.RemoveMember(this.FindMember(memberId));
            }
        }

        public void CreateDivision(Division division)
        {
            if (this.HasDivision(division.Name))
            {
                throw new InvalidOperationException();
            }

            Divisions.Add(division);
        }

        public Division RemoveDivision(DivisionId divisionId)
        {
            if (!this.HasDivision(divisionId))
            {
                throw new InvalidOperationException();
            }

            var division = Divisions.SingleOrDefault(x => x.Id == divisionId);

            Divisions.Remove(division);

            return division;
        }

        public void AddMember(IMemberInfo memberInfo)
        {
            if (this.HasMember(memberInfo.UserId))
            {
                throw new InvalidOperationException();
            }

            var member = new Member(memberInfo);

            Members.Add(member);

            this.AddDomainEvent(new ClanMemberAddedDomainEvent(member.UserId, Id));
        }

        public void Leave(Member member) //Francis on devrais avoir RemoveMember a la place et faire le check dans le service
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

            this.AddDomainEvent(new ClanMemberRemovedDomainEvent(member.UserId, Id));
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

        public bool HasDivision(string name)
        {
            return Divisions.Any(division => division.Name == name);
        }

        public bool HasDivision(DivisionId divisionId)
        {
            return Divisions.Any(division => division.Id == divisionId);
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

        public void Update(string? summary)
        {
            Summary = summary;
        }

        private void DelegateOwnership(Member member)
        {
            OwnerId = member.UserId;
        }
    }
}
