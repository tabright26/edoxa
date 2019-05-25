// Filename: ChallengeType.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class ChallengeType : Enumeration<ChallengeType>
    {
        public static readonly ChallengeType Type1 = new ChallengeType(1 << 0, nameof(Type1));
        public static readonly ChallengeType Type2 = new ChallengeType(1 << 1, nameof(Type2));
        public static readonly ChallengeType Type3 = new ChallengeType(1 << 2, nameof(Type3));
        public static readonly ChallengeType Type4 = new ChallengeType(1 << 3, nameof(Type4));
        public static readonly ChallengeType Type5 = new ChallengeType(1 << 4, nameof(Type5));
        public static readonly ChallengeType Type6 = new ChallengeType(1 << 5, nameof(Type6));

        private ChallengeType(int value, string name) : base(value, name)
        {
        }
    }
}
