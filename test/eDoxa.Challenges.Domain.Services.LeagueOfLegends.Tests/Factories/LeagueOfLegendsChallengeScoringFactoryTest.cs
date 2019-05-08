// Filename: LeagueOfLegendsChallengeScoringFactoryTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Factories
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeScoringFactoryTest
    {
        //[DataRow(ChallengeType.Default)]
        //[DataTestMethod]
        //public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        //{
        //    // Arrange
        //    var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

        //    // Act
        //    var strategy = factory.CreateScoring(new MockChallenge(type));

        //    // Assert
        //    strategy.Scoring.Should().NotBeNull();
        //}

        //[DataRow(ChallengeType.None)]
        //[DataRow(ChallengeType.All)]
        //[DataTestMethod]
        //public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type)
        //{
        //    // Arrange
        //    var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

        //    // Act
        //    var action = new Action(() => factory.CreateScoring(new MockChallenge(type)));

        //    // Assert
        //    action.Should().Throw<NotImplementedException>();
        //}

        //private sealed class MockChallenge : Challenge
        //{
        //    public MockChallenge(ChallengeType type) : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup())
        //    {
        //        Setup.SetPrivateField("_type", type);
        //    }
        //}
    }
}