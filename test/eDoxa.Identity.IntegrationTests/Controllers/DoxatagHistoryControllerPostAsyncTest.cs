// Filename: DoxatagHistoryControllerPostAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    public sealed class DoxatagHistoryControllerPostAsyncTest : IntegrationTest
    {
        public DoxatagHistoryControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(ChangeDoxatagRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/doxatag-history", request);
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();

            const string doxatagName = "Name";

            var factory = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<IUserService>();

                    await userManager.CreateAsync(user);

                    var doxatagService = scope.GetRequiredService<IDoxatagService>();

                    var result = await doxatagService.ChangeDoxatagAsync(user, doxatagName);

                    result.IsValid.Should().BeTrue();
                });

            // Act
            using var response = await this.ExecuteAsync(
                new ChangeDoxatagRequest
                {
                    Name = "New"
                });

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsStringAsync();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
