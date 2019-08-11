// Filename: DoxatagControllerGetAsyncTest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
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

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxatagControllerGetAsyncTest : IClassFixture<IdentityWebApplicationFactory>
    {
        public DoxatagControllerGetAsyncTest(IdentityWebApplicationFactory identityWebApplicationFactory)
        {
            User = new HashSet<User>(IdentityStorage.TestUsers).First();

            var factory = identityWebApplicationFactory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(services => services.AddTestMvc(new[] {new Claim(JwtClaimTypes.Subject, User.Id.ToString())}))
            );

            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatag");
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus200OK()
        {
            var doxatag = new Doxatag();

            User.Doxatag = doxatag;

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

                    var doxatagResponse = await response.DeserializeAsync<DoxatagResponse>();

                    doxatagResponse.Should().BeEquivalentTo(mapper.Map<DoxatagResponse>(doxatag));
                }
            );
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus204NoContent()
        {
            User.Doxatag = null;

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
