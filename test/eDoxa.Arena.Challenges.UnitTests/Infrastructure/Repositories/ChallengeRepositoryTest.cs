// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers.Assertions.Extensions;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers.Extensions;
using eDoxa.Seedwork.Testing;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Repositories
{
    public sealed class ChallengeRepositoryTest : UnitTest
    {
        public ChallengeRepositoryTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        [Fact]
        public async Task FetchChallengesAsync_FromRepository_ShouldNotBeNull()
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(1);

            var fakeChallenges = challengeFaker.FakeChallenges(5);

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                repository.Create(fakeChallenges);

                await repository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

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
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(1);

            var fakeChallenge = challengeFaker.FakeChallenge();

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                repository.Create(fakeChallenge);

                await repository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                //Act
                var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                //Assert
                challenge.Should().NotBeNull();
            }
        }
    }
}
