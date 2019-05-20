﻿// Filename: ScoringFactoryTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Services.Tests.Factories
{
    [TestClass]
    public sealed class ScoringFactoryTest
    {
        //private static readonly ScoringFactory ScoringFactory = ScoringFactory.Instance;

        //[DataRow(ChallengeType.Default, Game.LeagueOfLegends)]
        //[DataTestMethod]
        //public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type, Game game)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(type, game);

        //    // Act
        //    var strategy = ScoringFactory.CreateScoringStrategy(challenge);

        //    // Assert
        //    strategy.Scoring.Should().NotBeNull();
        //}

        //[DataRow(ChallengeType.Default, Game.CSGO)]
        //[DataRow(ChallengeType.Default, Game.Fortnite)]
        //[DataTestMethod]
        //public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type, Game game)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(type, game);

        //    // Act
        //    var action = new Action(() => ScoringFactory.CreateScoringStrategy(challenge));

        //    // Assert
        //    action.Should().Throw<NotImplementedException>();
        //}

        //private sealed class MockChallenge : Challenge
        //{
        //    public MockChallenge(ChallengeType type, Game game) : base(game, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup())
        //    {
        //        Setup.SetPrivateField("_type", type);
        //    }
        //}
    }
}