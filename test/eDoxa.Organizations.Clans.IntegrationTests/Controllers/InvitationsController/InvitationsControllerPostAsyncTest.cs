﻿// Filename: InvitationsControllerPostAsyncTest.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerPostAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        private readonly OrganizationsClansApiFactory _apiFactory;

        public InvitationsControllerPostAsyncTest(OrganizationsClansApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
            _httpClient = new HttpClient();
        }
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationPostRequest invitationPostRequest)
        {
            return await _httpClient.PostAsync("api/invitations", new JsonContent(invitationPostRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() //Bad request cause user already in the clan.
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
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

            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(userId, clan.Id));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
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

            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(new UserId(), clan.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
