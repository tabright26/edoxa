// Filename: ChallengeQueriesTest.cs
// Date Created: 2019-04-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Application.Queries;
using eDoxa.Challenges.Application.Tests.Asserts;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.DTO.Factories;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Seedwork.Infrastructure.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Application.Tests.Queries
{
    [TestClass]
    public sealed class ChallengeQueriesTest
    {        
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;
        private static readonly ChallengesMapperFactory ChallengesMapperFactory = ChallengesMapperFactory.Instance;

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeMapped()
        {
            var challenge = ChallengeAggregateFactory.CreateRandomChallenge();

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
                    var challengeDTO = await queries.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState.Opened);

                    // Assert
                    ChallengesAssert.IsMapped(challengeDTO);
                }
            }
        }
    }
}