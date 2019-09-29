// Filename: AddressBookControllerPutAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.IntegrationTests.TestHelpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{

    public sealed class AddressBookControllerPutAsyncTest : IntegrationTestClass
    {
        public AddressBookControllerPutAsyncTest(IdentityApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid addressId, AddressPutRequest request)
        {
            return await _httpClient.PutAsync($"api/address-book/{addressId}", new JsonContent(request));
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = await TestData.FileStorage.GetUsersAsync();
            var user = users.First();
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

                    result = await userManager.AddAddressAsync(
                        user,
                        "Canada",
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

                    var message = await response.DeserializeAsync<string>();

                    message.Should().NotBeNullOrWhiteSpace();
                });
        }
    }
}
