// Filename: PersonalName.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class PersonalName : ValueObject
    {
        public PersonalName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string FullName => this.ToString();

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
