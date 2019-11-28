// Filename: AddressBookControllerPutAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Requests;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class AddressBookControllerPutAsyncTest : IntegrationTest
    {
        public AddressBookControllerPutAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid addressId, AddressPutRequest request)
        {
            return await _httpClient.PutAsJsonAsync($"api/address-book/{addressId}", request);
        }

        private HttpClient _httpClient;

        [Fact(Skip = "Bearer authentication mock bug since .NET Core 3.0")]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();
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

                    result = await userManager.AddAddressAsync(
                        user,
                        Country.Canada,
                        "1234 Test Street",
                        null,
                        "Toronto",
                        "Ontario",
                        "A1A1A1");

                    result.Succeeded.Should().BeTrue();

                    var addressBook = await userManager.GetAddressBookAsync(user);

                    // Act
                    using var response = await this.ExecuteAsync(
                        addressBook.First().Id,
                        new AddressPutRequest(
                            "1234 Rue Test",
                            null,
                            "Montreal",
                            "Quebec",
                            "Z9Z9Z9"));

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);

                    var message = await response.Content.ReadAsStringAsync();

                    message.Should().NotBeNullOrWhiteSpace();
                });
        }
    }
}
