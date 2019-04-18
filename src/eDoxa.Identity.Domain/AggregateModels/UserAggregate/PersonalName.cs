// Filename: PersonalName.cs
// Date Created: 2019-04-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class PersonalName : ValueObject
    {
        private string _firstName;
        private string _lastName;

        public PersonalName(string firstName, string lastName) : this()
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        private PersonalName()
        {
            // Required by EF Core.
        }

        public string FirstName => _firstName;

        public string LastName => _lastName;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}