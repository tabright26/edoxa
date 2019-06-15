// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.Extensions;
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

            var challenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    repository.Create(challenge);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    challenge = await repository.FindChallengeAsync(challenge.Id);

                    challenge.Should().NotBeNull();

                    challenge.ShouldBeValidObjectState();

                    var challengeAsNoTracking = await repository.FindChallengeAsync(challenge.Id);

                    challengeAsNoTracking.Should().NotBeNull();

                    challengeAsNoTracking.ShouldBeValidObjectState();

                    challengeAsNoTracking.Should().BeEquivalentTo(challenge);
                }
            }
        }

        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataTestMethod]
        public async Task Create_Challenges_ShouldHaveCount(int count)
        {
            var challengeFaker = new ChallengeFaker();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    repository.Create(challengeFaker.Generate(count));

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    var challenges = await repository.FindChallengesAsync();

                    challenges.Should().HaveCount(count);

                    challenges.ShouldBeValidObjectState();

                    var challengesAsNoTracking = await repository.FindChallengesAsNoTrackingAsync();

                    challengesAsNoTracking.Should().HaveCount(count);

                    challengesAsNoTracking.ShouldBeValidObjectState();

                    challengesAsNoTracking.Should().BeEquivalentTo(challenges);
                }
            }
        }
    }
}
