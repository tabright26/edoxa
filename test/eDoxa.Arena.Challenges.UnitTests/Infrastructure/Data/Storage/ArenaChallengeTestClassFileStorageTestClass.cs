// Filename: ArenaChallengeTestClassFileStorageTestClass.cs
// Date Created: 2019-09-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Storage
{
    public sealed class ArenaChallengeTestClassFileStorageTestClass : UnitTestClass
    {
        public ArenaChallengeTestClassFileStorageTestClass(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetChallengesAsync_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var challengeFakerFactory = new FakerFactory();
            var fileStorage = new AzureFileStorage();
            var storage = new ArenaChallengeTestFileStorage(fileStorage, challengeFakerFactory);

            // Act
            var challenges = await storage.GetChallengesAsync();

            // Assert
            challenges.Should().HaveCount(40);
        }

        [Fact]
        public async Task GetUsersAsync_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var challengeFakerFactory = new FakerFactory();
            var fileStorage = new AzureFileStorage();
            var storage = new ArenaChallengeTestFileStorage(fileStorage, challengeFakerFactory);

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }

        [Fact]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var challengeFakerFactory = new FakerFactory();
            var fileStorage = new AzureFileStorage();
            var storage = new ArenaChallengeTestFileStorage(fileStorage, challengeFakerFactory);

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }
    }
}
