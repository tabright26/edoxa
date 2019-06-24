// Filename: ChallengeQueriesTest.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.IntegrationTests.Queries
{
    [TestClass]
    public sealed class ChallengeQueriesTest
    {
        private static readonly IMapper Mapper = MapperBuilder.CreateMapper();

        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeNull()
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1);

            var challenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    repository.Create(challenge);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var mock = new Mock<IHttpContextAccessor>();

                    mock.SetupClaims();

                    var challengeQuery = new ChallengeQuery(context, Mapper, mock.Object);

                    var challengeViewModel = await challengeQuery.FindChallengeAsync(challenge.Id);

                    challengeViewModel.Should().NotBeNull();

                    //challenge.Should().NotBeNull();

                    //challenge.ShouldBeValidObjectState();

                    //var challengeAsNoTracking = await repository.FindChallengeAsync(challenge.Id);

                    //challengeAsNoTracking.Should().NotBeNull();

                    //challengeAsNoTracking.ShouldBeValidObjectState();

                    //challengeAsNoTracking.Should().BeEquivalentTo(challenge);
                }
            }
        }
    }
}
