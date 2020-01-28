﻿// Filename: AddressBookControllerPutAsyncTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
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
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    public sealed class AddressBookControllerPutAsyncTest : IntegrationTest
    {
        public AddressBookControllerPutAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid addressId, UpdateAddressRequest request)
        {
            return await _httpClient.PutAsJsonAsync($"api/address-book/{addressId}", request);
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

                    await userManager.CreateAsync(user);

                    var addressService = scope.GetRequiredService<IAddressService>();

                    var result = await addressService.AddAddressAsync(
                        UserId.FromGuid(user.Id),
                        Country.Canada,
                        "1234 Test Street",
                        null,
                        "Toronto",
                        "ON",
                        "A1A 1A1");

                    result.IsValid.Should().BeTrue();

                    var addressBook = await addressService.GetAddressBookAsync(user);

                    // Act
                    using var response = await this.ExecuteAsync(
                        addressBook.First().Id,
                        new UpdateAddressRequest
                        {
                            CountryIsoCode = EnumCountryIsoCode.CA,
                            Line1 = "1234 Test Street",
                            Line2 = null,
                            City = "Toronto",
                            State = "ON",
                            PostalCode = "A1A 1A1"
                        });

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);

                    var message = await response.Content.ReadAsStringAsync();

                    message.Should().NotBeNullOrWhiteSpace();
                });
        }
    }
}
