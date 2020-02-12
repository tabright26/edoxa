// Filename: RegisterAccountAsyncTest.cs
// Date Created: 2020-01-28
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers.AccountController
{
    public sealed class RegisterAccountAsyncTest : IntegrationTest
    {
        public RegisterAccountAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(RegisterAccountRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/account/register", request);
        }

        [Fact(Skip = "Mocks needed.")]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            const string email = "test@edoxa.gg";
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost;
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();
            
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            var request = new RegisterAccountRequest
            {
                Country = EnumCountryIsoCode.CA,
                Dob = "1990/01/01",
                Email = email,
                Ip = "127.0.0.1",
                Password = "Pass@word1",
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(Skip = "Mocks needed.")]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var factory = TestHost;
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            const string email = "test@edoxa.gg";

            var request = new RegisterAccountRequest
            {
                Email = email,
                Password = "Pass@word1",
                Dob = "01/01/1990",
                Country = EnumCountryIsoCode.CA,
                Ip = "127.0.0.1"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    var user = await service.FindByEmailAsync(email);

                    user.Should().NotBeNull();
                });
        }
    }
}
