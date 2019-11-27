// Filename: UserInformations.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class UserInformations : ValueObject
    {
        public UserInformations(
            string firstName,
            string lastName,
            Gender gender,
            Dob dob
        ) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Dob = dob;
        }

        private UserInformations()
        {
            // Required by EF Core.
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Gender Gender { get; private set; }

        public Dob Dob { get; private set; }

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
