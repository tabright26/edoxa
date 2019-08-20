using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxaTagsControllerGetAsyncTest : IClassFixture<IdentityWebApiFactory>
    {
        public DoxaTagsControllerGetAsyncTest(IdentityWebApiFactory identityWebApiFactory)
        {
            _httpClient = identityWebApiFactory.CreateClient();
            _testServer = identityWebApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatags");
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContent()
        {
            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetAsync_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    foreach (var testUser in IdentityStorage.TestUsers.Take(100).ToList())
                    {
                        var result = await userManager.CreateAsync(testUser);

                        result.Succeeded.Should().BeTrue();
                    }
                }
            );

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var users = await response.DeserializeAsync<UserDoxaTagResponse[]>();

            users.Should().HaveCount(100);
        }
    }
}
