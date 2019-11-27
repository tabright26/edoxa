// Filename: DoxatagHistoryControllerPostAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxatagHistoryControllerPostAsyncTest : IntegrationTest
    {
        public DoxatagHistoryControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(DoxatagPostRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/doxatag-history", request);
        }

        private HttpClient _httpClient;

        [Fact(Skip = "Bearer authentication mock bug since .NET Core 3.0")]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();

            user.DoxatagHistory = new Collection<UserDoxatag>
            {
                new UserDoxatag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1000,
                    Timestamp = DateTime.UtcNow
                }
            };

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();
                });

            // Act
            using var response = await this.ExecuteAsync(new DoxatagPostRequest("New"));

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsStringAsync();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
