// Filename: ClansControllerPostAsyncTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using SQLitePCL;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClanLogoController
{
    public sealed class ClanLogoControllerPostAsyncTest : IntegrationTest
    {
        public ClanLogoControllerPostAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId, ClanLogoPostRequest clanLogoPostRequest)
        {
            return await _httpClient.PostAsync($"api/clans/{clanId}/logo", new JsonContent(clanLogoPostRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));
            //var file = Path.Combine(assemblyPath, "Setup/logo/edoxa.png")
            var formFile = new FormFile(
                file,
                0,
                file.Length,
                file.Name,
                file.Name);

            // Act
            using var response = await this.ExecuteAsync(new ClanId(), new ClanLogoPostRequest(formFile));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Not owner of clan bad request
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));
            //var file = Path.Combine(assemblyPath, "Setup/logo/edoxa.png")
            var formFile = new FormFile(
                file,
                0,
                file.Length,
                file.Name,
                file.Name);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new ClanId(), new ClanLogoPostRequest(formFile));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));

            //var file = Path.Combine(assemblyPath, "Setup/logo/edoxa.png")

            var formFile = new FormFile(
                file,
                0,
                file.Length,
                file.Name,
                file.Name);

            // Act
            using var response = await this.ExecuteAsync(new ClanId(), new ClanLogoPostRequest(formFile));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
