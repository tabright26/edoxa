﻿// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Assertions.Extensions;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Infrastructure.Repositories
{
    public sealed class ChallengeRepositoryTest : UnitTest
    {
        public ChallengeRepositoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task FetchChallengesAsync_FromRepository_ShouldNotBeNull()
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends);

            var fakeChallenges = challengeFaker.FakeChallenges(5);

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, TestMapper);

                repository.Create(fakeChallenges);

                await repository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, TestMapper);

                //Act
                var challenges = await repository.FetchChallengesAsync();

                //Assert
                challenges.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task FindChallengeAsync_FromRepository_ShouldNotBeNull()
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends);

            var fakeChallenge = challengeFaker.FakeChallenge();

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, TestMapper);

                repository.Create(fakeChallenge);

                await repository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, TestMapper);

                //Act
                var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                //Assert
                challenge.Should().NotBeNull();
            }
        }
    }
}