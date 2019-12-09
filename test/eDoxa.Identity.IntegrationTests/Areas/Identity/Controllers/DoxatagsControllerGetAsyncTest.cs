// Filename: DoxatagsControllerGetAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Responses;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class DoxatagsControllerGetAsyncTest : IntegrationTest
    {
        public DoxatagsControllerGetAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatags");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Arrange
            _httpClient = TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            _httpClient = TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<IUserService>();

                    var doxatagService = scope.GetRequiredService<IDoxatagRepository>();

                    var users = TestData.FileStorage.GetUsers();

                    var doxatags = TestData.FileStorage.GetDoxatags();

                    foreach (var testUser in users.Take(100).ToList())
                    {
                        var result = await userManager.CreateAsync(testUser);

                        result.Succeeded.Should().BeTrue();

                        doxatagService.Create(doxatags.Single(doxatag => doxatag.UserId == testUser.Id));

                        await doxatagService.UnitOfWork.CommitAsync();
                    }
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var doxatagResponses = await response.Content.ReadAsAsync<DoxatagResponse[]>();

            doxatagResponses.Should().HaveCount(100);
        }
    }
}
