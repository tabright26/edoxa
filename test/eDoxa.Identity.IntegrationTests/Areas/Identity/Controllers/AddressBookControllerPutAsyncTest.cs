﻿// Filename: AddressBookControllerPutAsyncTest.cs
// Date Created: 2019-08-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class AddressBookControllerPutAsyncTest : IClassFixture<IdentityWebApiFactory>
    {
        public AddressBookControllerPutAsyncTest(IdentityWebApiFactory identityWebApiFactory)
        {
            var identityStorage = new IdentityTestFileStorage();
            User = identityStorage.GetUsersAsync().GetAwaiter().GetResult().First();
            var factory = identityWebApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, User.Id.ToString()));
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid addressId, AddressPutRequest request)
        {
            return await _httpClient.PutAsync($"api/address-book/{addressId}", new JsonContent(request));
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        [Fact]
        public async Task ShouldBeStatus200Ok()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();

                    result = await userManager.AddAddressAsync(
                        User,
                        "Old",
                        "Old",
                        null,
                        "Old",
                        "Old",
                        "Old"
                    );

                    result.Succeeded.Should().BeTrue();

                    var addressBook = await userManager.GetAddressBookAsync(User);

                    // Act
                    using var response = await this.ExecuteAsync(
                        addressBook.First().Id,
                        new AddressPutRequest(
                            "New",
                            "New",
                            "New",
                            "New",
                            "New"
                        )
                    );

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(StatusCodes.Status200OK);

                    var message = await response.DeserializeAsync<string>();

                    message.Should().NotBeNullOrWhiteSpace();
                }
            );
        }
    }
}
