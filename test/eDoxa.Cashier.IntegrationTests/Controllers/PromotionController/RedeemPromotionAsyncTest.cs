// Filename: RedeemPromotionAsyncTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Routing;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers.PromotionController
{
    public sealed class RedeemPromotionAsyncTest : IntegrationTest
    {
        public RedeemPromotionAsyncTest(
            TestHostFixture testHost,
            TestDataFixture testData,
            TestMapperFixture testMapper,
            Func<HttpClient, LinkGenerator, object, Task<HttpResponseMessage>>? executeAsync = null
        ) : base(
            testHost,
            testData,
            testMapper,
            executeAsync)
        {
        }

        private const string TestCode = "123ABC";
        private HttpClient _httpClient;

        private static Promotion GenerateExpiredPromotion(Currency currency)
        {
            return new Promotion(
                TestCode,
                currency,
                TimeSpan.FromDays(0),
                new DateTimeProvider(DateTime.UtcNow));
        }

        private static Promotion GenerateValidPromotion(Currency currency)
        {
            return new Promotion(
                TestCode,
                currency,
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.UtcNow.AddDays(30)));
        }

        private async Task<HttpResponseMessage> ExecuteAsync(string promotionalCode)
        {
            return await _httpClient.PostAsJsonAsync($"api/promotions/{promotionalCode}", new {});
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var recipient = new PromotionRecipient(new User(user), new DateTimeProvider(DateTime.UtcNow));
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IPromotionRepository>();
                    repository.Create(GenerateExpiredPromotion(new Money(50)));
                    await repository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(TestCode);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IPromotionRepository>();
                    var promotion = await repository.FindPromotionOrNullAsync(TestCode);
                    promotion.Should().NotBeNull();
                    promotion?.IsRedeemBy(recipient).Should().BeFalse();
                });
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(TestCode);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var recipient = new PromotionRecipient(new User(user), new DateTimeProvider(DateTime.UtcNow));
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IPromotionRepository>();

                    repository.Create(GenerateValidPromotion(new Money(50)));

                    await repository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(TestCode);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IPromotionRepository>();

                    var promotion = await repository.FindPromotionOrNullAsync(TestCode);

                    promotion.Should().NotBeNull();
                    promotion?.IsRedeemBy(recipient).Should().BeTrue();
                });
        }
    }
}
