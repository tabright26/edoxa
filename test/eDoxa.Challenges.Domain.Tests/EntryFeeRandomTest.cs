// Filename: EntryFeeRandomTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests
{
    [TestClass]
    public sealed class EntryFeeRandomTest
    {
        private static readonly EntryFeeRandom Random = new EntryFeeRandom();

        [DataRow(0.25D, 0.25D)]
        [DataRow(0.25D, 5D)]
        [DataRow(0.25D, 100D)]
        [DataRow(0.25D, 1000D)]
        [DataRow(4.75D, 4.75D)]
        [DataRow(5D, 50D)]
        [DataRow(100D, 1000D)]
        [DataRow(1000D, 1500D)]
        [DataTestMethod]
        public void Next_EntryFee_ShouldBeInRange(double minValue, double maxValue)
        {
            // Arrange
            var minimumValue = new EntryFee(new decimal(minValue));
            var maximumValue = new EntryFee(new decimal(maxValue));

            // Act
            var entryFee = Random.Next(new EntryFeeRange(minimumValue, maximumValue));

            // Arrange
            entryFee.Should().BeInRange(new EntryFee(Math.Floor(minimumValue), false), new EntryFee(Math.Ceiling(maximumValue), false));
        }
    }
}