// Filename: ProfileControllerPatchAsyncTest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Contents;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class ProfileControllerPatchAsyncTest : IClassFixture<IdentityWebApplicationFactory>
    {
        public ProfileControllerPatchAsyncTest(IdentityWebApplicationFactory identityWebApplicationFactory)
        {
            User = new HashSet<User>(IdentityStorage.TestUsers).First();

            var factory = identityWebApplicationFactory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(services => services.AddFakeClaimsPrincipalFilter(new[] {new Claim(JwtClaimTypes.Subject, User.Id.ToString())}))
            );

            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private async Task<HttpResponseMessage> ExecuteAsync(JsonPatchDocument<ProfilePatchRequest> document)
        {
            return await _httpClient.PatchAsync("api/profile", new JsonPatchContent(document));
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        [Fact]
        public async Task PatchAsync_ShouldBeStatus200OK()
        {
            var profile = new Profile
            {
                FirstName = "Old"
            };

            User.Profile = profile;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            var document = new JsonPatchDocument<ProfilePatchRequest>();

            document.Test(request => request.FirstName, profile.FirstName);

            document.Add(request => request.FirstName, "New");

            // Act
            using var response = await this.ExecuteAsync(document);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var message = await response.DeserializeAsync<string>();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
