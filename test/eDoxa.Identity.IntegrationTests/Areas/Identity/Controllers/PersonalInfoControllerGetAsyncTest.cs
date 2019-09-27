// Filename: PersonalInfoControllerGetAsyncTest.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.IntegrationTests.Collections;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    [Collection(nameof(TestDataFixture.TestData))]
    public sealed class PersonalInfoControllerGetAsyncTest : IClassFixture<IdentityApiFactory>
    {
        public PersonalInfoControllerGetAsyncTest(IdentityApiFactory identityApiFactory, TestDataFixture testData)
        {
            _identityApiFactory = identityApiFactory;
            _testData = testData;
        }

        private readonly IdentityApiFactory _identityApiFactory;
        private readonly TestDataFixture _testData;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/personal-info");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            var users = await _testData.TestData.GetUsersAsync();
            var user = users.First();
            user.PersonalInfo = null;
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
            var users = await _testData.TestData.GetUsersAsync();
            var user = users.First();
            var profile = new UserPersonalInfo();
            user.PersonalInfo = profile;
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

                    var profileResponse = await response.DeserializeAsync<UserPersonalInfoResponse>();

                    profileResponse.Should().BeEquivalentTo(mapper.Map<UserPersonalInfoResponse>(profile));
                });
        }
    }
}
