// Filename: AddressBookControllerGetByIdAsyncTest.cs
// Date Created: 2019-08-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class AddressBookControllerGetByIdAsyncTest : IClassFixture<IdentityWebApiFactory>
    {
        public AddressBookControllerGetByIdAsyncTest(IdentityWebApiFactory identityWebApiFactory)
        {
            User = new HashSet<User>(IdentityStorage.TestUsers).First();
            var factory = identityWebApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, User.Id.ToString()));
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid addressId)
        {
            return await _httpClient.GetAsync($"api/address-book/{addressId}");
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus200OK()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();

                    result = await userManager.AddAddressAsync(
                        User,
                        "Test",
                        "Test",
                        null,
                        "Test",
                        "Test",
                        "Test"
                    );

                    result.Succeeded.Should().BeTrue();

                    var addressBook = await userManager.GetAddressBookAsync(User);

                    var address = addressBook.First();

                    // Act
                    using var response = await this.ExecuteAsync(address.Id);

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(StatusCodes.Status200OK);

                    var mapper = scope.GetRequiredService<IMapper>();

                    var addressResponse = await response.DeserializeAsync<AddressResponse>();

                    addressResponse.Should().BeEquivalentTo(mapper.Map<AddressResponse>(address));
                }
            );
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus404NotFound()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(Guid.NewGuid());

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
