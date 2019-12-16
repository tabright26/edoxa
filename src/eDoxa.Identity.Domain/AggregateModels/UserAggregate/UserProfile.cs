// Filename: UserInformations.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class UserProfile : ValueObject
    {
        public UserProfile(
            string firstName,
            string lastName,
            Gender gender,
            UserDob dob
        ) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Dob = dob;
        }

        private UserProfile()
        {
            // Required by EF Core.
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Gender Gender { get; private set; }

        public UserDob Dob { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
            yield return Gender;
            yield return Dob;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
