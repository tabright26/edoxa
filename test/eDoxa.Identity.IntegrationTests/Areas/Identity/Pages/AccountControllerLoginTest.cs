﻿// Filename: AccountControllerLoginTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.IntegrationTests.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Pages
{
    public sealed class AccountControllerLoginTest : IClassFixture<TestApiFactory>
    {
        public AccountControllerLoginTest(TestApiFactory testApiFactory)
        {
            _httpClient = testApiFactory.CreateClient();
        }

        private readonly HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("identity/account/login");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
