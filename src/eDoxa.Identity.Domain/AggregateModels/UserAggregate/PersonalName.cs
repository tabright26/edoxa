// Filename: PersonalName.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

using IdentityModel;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class PersonalName : ValueObject
    {
        private string _firstName;
        private string _lastName;

        public PersonalName(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName => _firstName;

        public string LastName => _lastName;

        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }

        public IEnumerable<UserClaim> ToClaims()
        {
            yield return new UserClaim
            {
                ClaimType = JwtClaimTypes.GivenName,
                ClaimValue = FirstName
            };

            yield return new UserClaim
            {
                ClaimType = JwtClaimTypes.FamilyName,
                ClaimValue = LastName
            };

            yield return new UserClaim
            {
                ClaimType = JwtClaimTypes.Name,
                ClaimValue = this.ToString()
            };
        }
    }
}