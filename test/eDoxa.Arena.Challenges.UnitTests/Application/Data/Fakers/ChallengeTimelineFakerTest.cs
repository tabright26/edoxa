// Filename: TimelineFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ChallengeTimelineFakerTest
    {
        public static IEnumerable<object[]> Data => ChallengeState.GetAll().Select(state => new object[] { state });

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeTimeline(ChallengeState state)
        {
            // Arrange
            var timelineFaker = new ChallengeTimelineFaker();

            // Act
            var timeline = timelineFaker.FakeTimeline(state);

            // Assert
            timeline.State.Should().Be(state);
        }
    }
}
