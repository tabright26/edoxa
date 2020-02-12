// Filename: CreatePromotionAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using IdentityModel;

using Microsoft.AspNetCore.Routing;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers.PromotionController
{
    public sealed class CreatePromotionAsyncTest : IntegrationTest
    {
        public CreatePromotionAsyncTest(
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

        private async Task<HttpResponseMessage> ExecuteAsync(CreatePromotionRequest request)
        {
            return await _httpClient.CustomPostAsJsonAsync("api/promotions", request);
        }

        private static CreatePromotionRequest GenerateRequest()
        {
            return new CreatePromotionRequest
            {
                Currency = new CurrencyDto
                {
                    Amount = new DecimalValue(50),
                    Type = EnumCurrencyType.Money
                },
                Duration = TimeSpan.FromDays(30).ToDuration(),
                ExpiredAt = DateTime.UtcNow.AddDays(30).ToTimestamp(),
                PromotionalCode = TestCode
            };
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            var user = TestData.FileStorage.GetUsers().First();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var request = new CreatePromotionRequest
            {
                Currency = new CurrencyDto
                {
                    Amount = new DecimalValue(50)
                },
                Duration = TimeSpan.FromDays(30).ToDuration(),
                ExpiredAt = DateTime.UtcNow.AddDays(30).ToTimestamp()
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var promotionRepository = scope.GetRequiredService<IPromotionRepository>();

                    var promotion = await promotionRepository.FindPromotionOrNullAsync(TestCode);

                    promotion.Should().BeNull();
                });
        }

        [Fact] // Francis: Json conversion bug when entering controller.
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = TestData.FileStorage.GetUsers().First();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var request = GenerateRequest();

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var promotion = await response.Content.ReadAsJsonAsync<PromotionDto>();

            promotion.Should().NotBeNull();
            promotion.Currency.Amount.Should().Be(request.Currency.Amount);
            promotion.Currency.Type.Should().Be(request.Currency.Type);
            promotion.PromotionalCode.Should().Be(request.PromotionalCode);
        }
    }
}
