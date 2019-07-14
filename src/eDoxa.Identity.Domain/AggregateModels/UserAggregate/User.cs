// Filename: User.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>
    {
        private readonly HashSet<Role> _roles = new HashSet<Role>();
        private readonly HashSet<Claim> _claims = new HashSet<Claim>();

        public User(
            Gamertag gamertag,
            Email email,
            BirthDate birthDate,
            PersonalName personalName,
            HashedPassword password
        ) : this(gamertag, email, birthDate, personalName)
        {
            Password = password;
        }

        public User(
            Gamertag gamertag,
            Email email,
            BirthDate birthDate,
            PersonalName personalName
        )
        {
            Gamertag = gamertag;
            Email = email;
            BirthDate = birthDate;
            PersonalName = personalName;
        }

        public Gamertag Gamertag { get; }

        public Email Email { get; }

        public PersonalName PersonalName { get; }

        public BirthDate BirthDate { get; }

        public Phone Phone { get; private set; }

        public Password Password { get; private set; }

        public IReadOnlyCollection<Role> Roles => _roles;

        public IReadOnlyCollection<Claim> Claims => _claims;

        public void AddClaim(Claim claim)
        {
            _claims.Add(claim);
        }

        public void RemoveClaim(Claim claim)
        {
            _claims.Remove(claim);
        }

        public void AddRole(Role role)
        {
            _roles.Add(role);
        }

        public void LinkPhone(Phone phone)
        {
            Phone = phone;
        }

        public void HashPassword(string password)
        {
            Password = new HashPassword(this, password);
        }
    }
}
