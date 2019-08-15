﻿// Filename: UserDoxatagControllerGetAsyncTest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class UserDoxaTagControllerGetAsyncTest : IClassFixture<IdentityWebApplicationFactory>
    {
        public UserDoxaTagControllerGetAsyncTest(IdentityWebApplicationFactory identityWebApplicationFactory)
        {
            User = new HashSet<User>(IdentityStorage.TestUsers).First();
            _httpClient = identityWebApplicationFactory.CreateClient();
            _testServer = identityWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        private User User { get; }

        private async Task<HttpResponseMessage> ExecuteAsync(Guid userId)
        {
            return await _httpClient.GetAsync($"api/users/{userId}/doxa-tag");
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus200OK()
        {
            var doxaTag = new DoxaTag
            {
                Name = "Test",
                Code = 12345
            };

            User.DoxaTag = doxaTag;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(User.Id);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var mapper = scope.GetRequiredService<IMapper>();

                    var doxaTagResponse = await response.DeserializeAsync<DoxaTagResponse>();

                    doxaTagResponse.Should().BeEquivalentTo(mapper.Map<DoxaTagResponse>(doxaTag));
                }
            );
        }

        [Fact]
        public async Task GetAsync_ShouldBeStatus204NoContent()
        {
            User.DoxaTag = null;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(User);

                    result.Succeeded.Should().BeTrue();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(User.Id);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}