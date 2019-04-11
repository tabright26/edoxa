﻿// Filename: ChallengeScoringTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeScoringTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Scoring_ShouldBeAssignableToType()
        {
            // Arrange
            var scoring = _factory.CreateChallengeScoring();
            
            // Act
            var type = typeof(Dictionary<string, float>);

            // Assert
            scoring.Should().BeAssignableTo(type);
        }
    }
}