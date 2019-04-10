// Filename: UserTag.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class UserTag : ValueObject
    {
        public UserTag(string username) : this()
        {
            Name = username;
            ReferenceNumber = GenerateReferenceNumber();
        }

        private UserTag()
        {
            // Required by EF Core.
        }

        public string Name { get; private set; }

        public short ReferenceNumber { get; private set; }

        public void ChangeTag(string username)
        {
            Name = username;
            ReferenceNumber = GenerateReferenceNumber();
        }

        public override string ToString()
        {
            return $"{Name}#{ReferenceNumber:00000}";
        }

        private static short GenerateReferenceNumber()
        {
            var random = new Random();
            return (short) random.Next(1, short.MaxValue);
        }
    }
}