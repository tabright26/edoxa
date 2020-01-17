// Filename: AddressBookControllerPostAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;
using Xunit.Abstractions;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    public sealed class AddressBookControllerPostAsyncTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AddressBookControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper, ITestOutputHelper testOutputHelper) : base(
            testHost,
            testData,
            testMapper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private async Task<HttpResponseMessage> ExecuteAsync(CreateAddressRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/address-book", request);
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();
            var factory = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<IUserService>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();

                    // Act
                    using var response = await this.ExecuteAsync(
                        new CreateAddressRequest
                        {
                            CountryIsoCode = EnumCountryIsoCode.CA,
                            Line1 = "1234 Test Street",
                            Line2 = null,
                            City = "Toronto",
                            State = "ON",
                            PostalCode = "A1A 1A1"
                        });

                    // Assert
                    var message = await response.Content.ReadAsStringAsync();

                    _testOutputHelper.WriteLine(message);

                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);

                    //var message = await response.Content.ReadAsStringAsync();

                    //message.Should().NotBeNullOrWhiteSpace();
                });
        }
    }
}
