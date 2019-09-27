// Filename: DoxaTagHistoryControllerGetAsyncTest.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxaTagHistoryControllerGetAsyncTest : IClassFixture<IdentityApiFactory>
    {
        public DoxaTagHistoryControllerGetAsyncTest(IdentityApiFactory identityApiFactory)
        {
            _identityApiFactory = identityApiFactory;
        }

        private readonly IdentityApiFactory _identityApiFactory;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatag-history");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            var identityStorage = new IdentityTestFileStorage(new AzureFileStorage());
            var users = await identityStorage.GetUsersAsync();
            var user = users.First();
            user.DoxaTagHistory = null;
            var factory = _identityApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var identityStorage = new IdentityTestFileStorage(new AzureFileStorage());
            var users = await identityStorage.GetUsersAsync();
            var user = users.First();

            user.DoxaTagHistory = new Collection<UserDoxaTag>
            {
                new UserDoxaTag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1000,
                    Timestamp = DateTime.UtcNow
                }
            };

            var factory = _identityApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var mapper = scope.GetRequiredService<IMapper>();

                    var doxaTagResponse = (await response.DeserializeAsync<IEnumerable<UserDoxaTagResponse>>()).First();

                    var expectedDoxaTagResponse = mapper.Map<IEnumerable<UserDoxaTagResponse>>(user.DoxaTagHistory).First();

                    doxaTagResponse.Name.Should().Be(expectedDoxaTagResponse.Name);

                    doxaTagResponse.Code.Should().Be(expectedDoxaTagResponse.Code);
                });
        }
    }
}
