// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class ChallengeRepositoryTest
    {
        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeNull()
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1);

            var fakeChallenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    repository.Create(fakeChallenge);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    challenge.Should().NotBeNull();
                }
            }
        }

        [TestMethod]
        public async Task Create_Challenges_ShouldNotBeNull()
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1);

            var fakeChallenges = challengeFaker.Generate(5);

            using (var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    repository.Create(fakeChallenges);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    var challenges = await repository.FetchChallengesAsync();

                    challenges.Should().NotBeNull();
                }
            }
        }
    }
}
