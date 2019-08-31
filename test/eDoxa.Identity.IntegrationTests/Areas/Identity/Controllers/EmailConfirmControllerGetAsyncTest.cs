// Filename: EmailConfirmControllerGetAsyncTest.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class EmailConfirmControllerGetAsyncTest : IClassFixture<IdentityApiFactory>
    {
        public EmailConfirmControllerGetAsyncTest(IdentityApiFactory identityApiFactory)
        {
            var identityStorage = new IdentityTestFileStorage();
            User = identityStorage.GetUsersAsync().GetAwaiter().GetResult().First();
            var factory = identityApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, User.Id.ToString()));
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        private async Task<HttpResponseMessage> ExecuteAsync(string userId, string code)
        {
            return await _httpClient.GetAsync($"api/email/confirm?userId={userId}&code={code}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(User);

                    // Act
                    using var response = await this.ExecuteAsync(User.Id.ToString(), code);

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                }
            );
        }
    }
}
