// Filename: TestModeParticipantQuantity.cs
// Date Created: 2019-06-03
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
    public sealed class TestModeParticipantQuantity : Enumeration<TestModeParticipantQuantity>
    {
        public static readonly TestModeParticipantQuantity Empty = new TestModeParticipantQuantity(1, nameof(Empty));
        public static readonly TestModeParticipantQuantity HalfFull = new TestModeParticipantQuantity(2, nameof(HalfFull));
        public static readonly TestModeParticipantQuantity Fulfilled = new TestModeParticipantQuantity(3, nameof(Fulfilled));

        public TestModeParticipantQuantity()
        {
        }

        private TestModeParticipantQuantity(int value, string name) : base(value, name)
        {
        }
    }
}
