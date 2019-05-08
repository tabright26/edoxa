// Filename: ChallengeType.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class ChallengeType : Enumeration<ChallengeType>
    {
        public static readonly ChallengeType Default = new ChallengeType(1 << 0, nameof(Default));
        public static readonly ChallengeType Random = new ChallengeType(1 << 1, nameof(Random));
        public static readonly ChallengeType Custom = new ChallengeType(1 << 2, nameof(Custom));

        private ChallengeType(int value, string name) : base(value, name)
        {
        }
    }
}