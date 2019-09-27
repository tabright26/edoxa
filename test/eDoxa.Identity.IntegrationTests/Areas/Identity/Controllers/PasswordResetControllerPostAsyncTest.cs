﻿// Filename: PasswordResetControllerPostAsyncTest.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class PasswordResetControllerPostAsyncTest : IClassFixture<IdentityApiFactory>
    {
        public PasswordResetControllerPostAsyncTest(IdentityApiFactory identityApiFactory)
        {
            _identityApiFactory = identityApiFactory;
        }

        private readonly IdentityApiFactory _identityApiFactory;

        private async Task<HttpResponseMessage> ExecuteAsync(PasswordResetPostRequest request)
        {
            return await _httpClient.PostAsync("api/password/reset", new JsonContent(request));
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var identityStorage = new IdentityTestFileStorage(new AzureFileStorage());
            var users = await identityStorage.GetUsersAsync();
            var user = users.First();

            var factory = _identityApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    // Arrange
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();

                    var code = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Act
                    using var response = await this.ExecuteAsync(new PasswordResetPostRequest("admin@edoxa.gg", "Pass@word1", code));

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                });
        }
    }
}
