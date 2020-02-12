// Filename: FindPhoneAsyncTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers.PhoneController
{
    public sealed class FindPhoneAsyncTest : IntegrationTest
    {
        public FindPhoneAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/phone");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var phoneNumber = await response.Content.ReadAsJsonAsync<PhoneDto>();
            phoneNumber.Should().Be(user.PhoneNumber);
        }
    }
}
