// Filename: TestModeMatchQuantity.cs
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
    public sealed class TestModeMatchQuantity : Enumeration<TestModeMatchQuantity>
    {
        public static readonly TestModeMatchQuantity Under = new TestModeMatchQuantity(1, nameof(Under));
        public static readonly TestModeMatchQuantity Exact = new TestModeMatchQuantity(2, nameof(Exact));
        public static readonly TestModeMatchQuantity Over = new TestModeMatchQuantity(3, nameof(Over));

        public TestModeMatchQuantity()
        {
        }

        private TestModeMatchQuantity(int value, string name) : base(value, name)
        {
        }
    }
}
