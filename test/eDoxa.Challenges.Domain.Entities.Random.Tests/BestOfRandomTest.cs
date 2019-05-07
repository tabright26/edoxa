// Filename: BestOfRandomTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Random.Tests
{
    [TestClass]
    public sealed class BestOfRandomTest
    {
        private static readonly BestOfRandom Random = new BestOfRandom();

        [DataRow(1, 7)]
        [DataRow(3, 5)]
        [DataTestMethod]
        public void Next_BestOf_ShouldBeInRange(int minValue, int maxValue)
        {
            // Arrange
            var minimumValue = new BestOf(minValue);
            var maximumValue = new BestOf(maxValue);

            // Act
            var bestOf = Random.Next(new BestOfRange(minimumValue, maximumValue));

            // Assert
            bestOf.Should().BeInRange(minimumValue, maximumValue);
        }
    }
}