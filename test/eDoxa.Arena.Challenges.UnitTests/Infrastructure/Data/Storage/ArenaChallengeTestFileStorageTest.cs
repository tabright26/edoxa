// Filename: ArenaChallengeTestFileStorageTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class ArenaChallengeTestFileStorageTest
    {
        [TestMethod]
        public async Task GetChallengesAsync_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var challengeFakerFactory = new ChallengeFakerFactory();
            var storage = new ArenaChallengeTestFileStorage(challengeFakerFactory);

            // Act
            var challenges = await storage.GetChallengesAsync();

            // Assert
            challenges.Should().HaveCount(40);
        }

        [TestMethod]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var challengeFakerFactory = new ChallengeFakerFactory();
            var storage = new ArenaChallengeTestFileStorage(challengeFakerFactory);

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }

        [TestMethod]
        public async Task GetUsersAsync_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var challengeFakerFactory = new ChallengeFakerFactory();
            var storage = new ArenaChallengeTestFileStorage(challengeFakerFactory);

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }
    }
}
