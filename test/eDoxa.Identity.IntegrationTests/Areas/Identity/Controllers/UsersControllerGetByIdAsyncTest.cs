// Filename: UsersControllerGetByIdAsyncTest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class UsersControllerGetByIdAsyncTest : IClassFixture<IdentityWebApplicationFactory>
    {
        public UsersControllerGetByIdAsyncTest(IdentityWebApplicationFactory identityWebApplicationFactory)
        {
            _httpClient = identityWebApplicationFactory.CreateClient();
            _testServer = identityWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(Guid userId)
        {
            return await _httpClient.GetAsync($"api/users/{userId}");
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus200OK()
        {
            // Arrange
            var user = IdentityStorage.TestUsers.First();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(user.Id);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var mapper = scope.GetRequiredService<IMapper>();

                    var userResponse = await response.DeserializeAsync<UserResponse>();

                    userResponse.Should().BeEquivalentTo(mapper.Map<UserResponse>(user));
                }
            );
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus404NotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(Guid.NewGuid());

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);

            var message = await response.DeserializeAsync<string>();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
