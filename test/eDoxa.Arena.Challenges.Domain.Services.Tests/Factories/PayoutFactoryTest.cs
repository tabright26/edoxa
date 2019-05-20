// Filename: ChallengePayoutFactoryTest.cs
// Date Created: 2019-05-03
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
    public sealed class PayoutFactoryTest
    {
        //private static readonly PayoutFactory PayoutFactory = PayoutFactory.Instance;

        //[DataRow(ChallengeType.Default)]
        //[DataTestMethod]
        //public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(type);

        //    // Act
        //    var strategy = PayoutFactory.CreatePayout(challenge);

        //    // Assert
        //    strategy.Payout.Should().NotBeNull();
        //}

        //[DataRow(ChallengeType.None)]
        //[DataRow(ChallengeType.All)]
        //[DataTestMethod]
        //public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type)
        //{
        //    // Arrange
        //    var challenge = new MockChallenge(type);

        //    // Act
        //    var action = new Action(() => PayoutFactory.CreatePayout(challenge));

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