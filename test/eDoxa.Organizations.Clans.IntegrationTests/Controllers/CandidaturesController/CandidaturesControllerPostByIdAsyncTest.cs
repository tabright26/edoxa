﻿// Filename: ChallengesControllerGetAsyncTest.cs
// Date Created: 2019-08-18
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerPostByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        public CandidaturesControllerPostByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.PostAsync($"api/candidatures/{candidatureId}", new JsonContent(""));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(new CandidatureId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var ownerId = new UserId();
            var candidatureId = new CandidatureId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    clanRepository.Create(new Clan("TestClan", ownerId));
                    await clanRepository.CommitAsync();

                    var clans = await clanRepository.FetchClansAsync();
                    var clan = clans.SingleOrDefault();

                    candidatureRepository.Create(new Candidature(new UserId(), clan != null ? clan.Id : new ClanId()));
                    await candidatureRepository.CommitAsync();

                    var candidatures = await candidatureRepository.FetchAsync();
                    var candidature = candidatures.SingleOrDefault();

                    if (candidature != null)
                    {
                        candidatureId = candidature.Id;
                    }
                }
            );

            // Act
            using var response = await this.ExecuteAsync(candidatureId != null ? candidatureId : new CandidatureId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Do I need to test out all single bad request possible ?

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() //Clan does not exist bad request.
        {
            // Arrange
            var candidatureId = new CandidatureId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    candidatureRepository.Create(new Candidature(new UserId(), new ClanId()));
                    await candidatureRepository.CommitAsync();

                    var candidatures = await candidatureRepository.FetchAsync();
                    var candidature = candidatures.SingleOrDefault();

                    if (candidature != null)
                    {
                        candidatureId = candidature.Id;
                    }
                }
            );

            // Act
            using var response = await this.ExecuteAsync(candidatureId != null ? candidatureId : new CandidatureId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
