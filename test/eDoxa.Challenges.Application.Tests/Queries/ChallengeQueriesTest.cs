// Filename: ChallengeQueriesTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Queries;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.DTO.Factories;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Challenges.Tests.Asserts;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Seedwork.Infrastructure.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Application.Tests.Queries
{
    [TestClass]
    public sealed class ChallengeQueriesTest
    {
        private static readonly FakeRandomChallengeFactory FakeRandomChallengeFactory = FakeRandomChallengeFactory.Instance;
        private static readonly ChallengesMapperFactory ChallengesMapperFactory = ChallengesMapperFactory.Instance;

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeMapped()
        {
            var challenge = FakeRandomChallengeFactory.CreateRandomChallenge();

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
                    // Arrange
                    var queries = new ChallengeQueries(context, ChallengesMapperFactory.CreateMapper());

                    // Act
                    var challengeDTO = await queries.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState1.Opened);

                    // Assert
                    ChallengeQueryAssert.IsMapped(challengeDTO.Single());
                }
            }
        }
    }
}