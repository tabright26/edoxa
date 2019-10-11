// Filename: ClanLogoControllerPostAsyncTest.cs
// Date Created: 2019-10-07
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClanLogoController
{
    public sealed class ClanLogoControllerPostAsyncTest : IntegrationTest
    {
        public ClanLogoControllerPostAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId, FileStream file)
        {
            return await _httpClient.PostAsync(
                $"api/clans/{clanId}/logo",
                new MultipartFormDataContent
                {
                    {new StreamContent(file), "logo", "edoxa.png"}
                });
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

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();

                });

            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));

            // Act
            using var response = await this.ExecuteAsync(clan.Id, file);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new ClanId(), file);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();
            var file = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Setup/edoxa.png"));

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clan.Id, file);

            // Assert
            await testServer.UsingScopeAsync(async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    var dbData = await clanRepository.DownloadLogoAsync(clan.Id);

                    var dbImageData = new MemoryStream();
                    var fileImageData = new MemoryStream();

                    dbData.Position = 0;
                    await dbData.CopyToAsync(dbImageData);

                    file.Position = 0;
                    await file.CopyToAsync(fileImageData);

                    dbImageData.ToArray().Should().BeEquivalentTo(fileImageData.ToArray());
                });

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);


        }
    }
}
