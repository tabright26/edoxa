// Filename: ChangePhoneAsyncTest.cs
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
using eDoxa.Grpc.Protos.Identity.Requests;
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
    public sealed class ChangePhoneAsyncTest : IntegrationTest
    {
        public ChangePhoneAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ChangePhoneRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/phone", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            var request = new ChangePhoneRequest
            {
                Number = string.Empty
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            const string phoneNumber = "4181112222";
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            var request = new ChangePhoneRequest
            {
                Number = phoneNumber
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var phone = await response.Content.ReadAsJsonAsync<PhoneDto>();
            phone.Number.Should().Be(phoneNumber);
        }
    }
}
