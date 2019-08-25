// Filename: DoxatagControllerGetAsyncTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed class DoxaTagHistoryControllerGetAsyncTest : IClassFixture<IdentityWebApiFactory>
    {
        public DoxaTagHistoryControllerGetAsyncTest(IdentityWebApiFactory identityWebApiFactory)
        {
            var identityStorage = new IdentityTestFileStorage();
            User = identityStorage.GetUsersAsync().GetAwaiter().GetResult().First();
            var factory = identityWebApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, User.Id.ToString()));
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatag-history");
        }

        [Fact]
        public async Task ShouldBeStatus200Ok()
        {
            User.DoxaTagHistory = new Collection<UserDoxaTag>
            {
                new UserDoxaTag
                {
                    Id = Guid.NewGuid(),
                    UserId = User.Id,
                    Name = "Test",
                    Code = 1000,
                    Timestamp = DateTime.UtcNow
                }
            };

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var mapper = scope.GetRequiredService<IMapper>();

                    var doxaTagResponse = (await response.DeserializeAsync<IEnumerable<UserDoxaTagResponse>>()).First();

                    var expectedDoxaTagResponse = mapper.Map<IEnumerable<UserDoxaTagResponse>>(User.DoxaTagHistory).First();

                    doxaTagResponse.Name.Should().Be(expectedDoxaTagResponse.Name);

                    doxaTagResponse.Code.Should().Be(expectedDoxaTagResponse.Code);
                }
            );
        }

        [Fact]
        public async Task ShouldBeStatus204NoContent()
        {
            User.DoxaTagHistory = null;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
