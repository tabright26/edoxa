// Filename: DoxaTagHistoryControllerPostAsyncTest.cs
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
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    [Collection(nameof(ControllerCollection))]
    public sealed class DoxaTagHistoryControllerPostAsyncTest : ControllerTest
    {
        public DoxaTagHistoryControllerPostAsyncTest(IdentityApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(DoxaTagPostRequest request)
        {
            return await _httpClient.PostAsync("api/doxatag-history", new JsonContent(request));
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = await TestData.FileStorage.GetUsersAsync();
            var user = users.First();

            user.DoxaTagHistory = new Collection<UserDoxaTag>
            {
                new UserDoxaTag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1000,
                    Timestamp = DateTime.UtcNow
                }
            };

            var factory = ApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
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
            using var response = await this.ExecuteAsync(new DoxaTagPostRequest("New"));

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.DeserializeAsync<string>();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
