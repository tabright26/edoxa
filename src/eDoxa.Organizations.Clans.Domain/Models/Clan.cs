// Filename: ClanModel.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

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
            Members = new List<Member>{ new Member(Id, ownerId) };

            //Todo : Resharper tells us to make class sealed ???
        }

        private Clan()
        {
            //Requied by EF Core
        }

        public string Name { get; private set; }

        public string? Summary { get; private set; }

        public UserId OwnerId { get; private set; }

        public ICollection<Member> Members { get; private set; }

        public bool AddMember(IMemberInfo memberInfo) //Not garantee to do something
        {
            if (Members.Any(member => member.UserId == memberInfo.UserId))
            {
                return false;
            }

            Members.Add(new Member(memberInfo));
            return true;
        }

        /// <summary>
        /// Remove a non owner member from the clan.
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns>True if a member was removed, false if not</returns>
        public bool RemoveNonOwnerMember(MemberId memberId) //Not garantee to do something
        {
            var memberToRemove = Members.SingleOrDefault(member => member.Id == memberId);

            if (memberToRemove == null || memberToRemove.UserId == OwnerId)
            {
                return false;
            }

            Members.Remove(memberToRemove);
            return true;

        }

        public void RemoveUser(UserId userId) //Garantee to do something
        {
            var memberToRemove = Members.SingleOrDefault(member => member.UserId == userId);

            if (memberToRemove == null)
            {
                return;
            }

            Members.Remove(memberToRemove);

            if (memberToRemove.UserId == OwnerId && Members.Any()) //If the owner left and there is members left
            {
                this.DelegateOwnership(Members.First());
            }

        }

        public void Update(string name, string summary)
        {
            Name = name;
            Summary = summary;
        }

        public bool IsOwner(UserId userId)
        {
            return userId == OwnerId;
        }

        public bool IsEmpty()
        {
            return !Members.Any();
        }

        private void DelegateOwnership(Member member)
        {
            OwnerId = member.UserId;
        }
    }
}
