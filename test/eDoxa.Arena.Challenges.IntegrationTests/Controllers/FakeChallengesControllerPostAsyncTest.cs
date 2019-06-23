// Filename: FakeChallengesControllerPostAsyncTest.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Application.Http;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class FakeChallengesControllerPostAsyncTest
    {
        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;

        public async Task<HttpResponseMessage> ExecuteAsync(FakeChallengesCommand command)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())})
                .PostAsync("api/fake/challenges", new JsonContent(command));
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Challenges.RemoveRange(_dbContext.Challenges);

            await _dbContext.SaveChangesAsync();
        }

        [DataRow(2, 100)]
        [DataRow(5, 1000)]
        [DataTestMethod]
        public async Task Status200Ok(int count, int seed)
        {
            var command = new FakeChallengesCommand(count, seed);

            var response = await this.ExecuteAsync(command);

            response.EnsureSuccessStatusCode();
        }

        //[DataRow(2, 100)]
        //[DataRow(5, 1000)]
        //[DataRow(10, 10000)]
        //[DataTestMethod]
        //public async Task Status400BadRequest(int count, int seed)
        //{
        //    var challengeFaker = new ChallengeFaker();

        //    challengeFaker.UseSeed(seed);

        //    var challenges = challengeFaker.Generate(count);

        //    _dbContext.Challenges.AddRange(challenges);

        //    await _dbContext.SaveChangesAsync();

        //    var command = new FakeChallengesCommand(count, seed);

        //    var response = await this.ExecuteAsync(command);

        //    response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        //    var validationProblemDetails = await response.DeserializeAsync<ValidationProblemDetails>();

        //    validationProblemDetails.Should().NotBeNull();

        //    validationProblemDetails?.Errors.First().Value.First().Should().Be($"This seed was already used: {seed}.");
        //}
    }
}
