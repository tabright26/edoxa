// Filename: ChangePhoneAsyncTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers.PhoneController
{
    public sealed class ChangePhoneAsyncTest : IntegrationTest
    {
        public ChangePhoneAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        // Francis: Internal server error pour tous les tests dans le identity en integrations.

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ChangePhoneRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/phone", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var request = new ChangePhoneRequest
            {
                Number = "4181112222"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = TestData.FileStorage.GetUsers().First();

            //var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()))
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestContainer<ContainerBuilder>(
                            container =>
                            {
                                var mockUserService = new Mock<IUserService>();
                                container.RegisterInstance(mockUserService.Object).As<IUserService>().SingleInstance();
                            });
                    });

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            var request = new ChangePhoneRequest
            {
                Number = "4181112222"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<UserRepository>();

                    var phoneNumber = await repository.GetPhoneNumberAsync(user);
                    phoneNumber.Should().Be(request.Number);
                });
        }
    }
}
