// Filename: DoxatagHistoryControllerGetAsyncTest.cs
// Date Created: 2019-09-16
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

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Responses;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxatagHistoryControllerGetAsyncTest : IntegrationTest
    {
        public DoxatagHistoryControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatag-history");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();
            user.DoxatagHistory = null;
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
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
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();

            user.DoxatagHistory = new Collection<UserDoxatag>
            {
                new UserDoxatag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1000,
                    Timestamp = DateTime.UtcNow
                }
            };

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
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

                    var doxatagResponse = (await response.DeserializeAsync<IEnumerable<UserDoxatagResponse>>()).First();

                    var expectedDoxatagResponse = mapper.Map<IEnumerable<UserDoxatagResponse>>(user.DoxatagHistory).First();

                    doxatagResponse.Name.Should().Be(expectedDoxatagResponse.Name);

                    doxatagResponse.Code.Should().Be(expectedDoxatagResponse.Code);
                });
        }
    }
}
