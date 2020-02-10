// Filename: CreatePromotionAsyncTest.cs
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

using Bogus.DataSets;

using eDoxa.Cashier.Domain.AggregateModels;
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
    public sealed class
        CreatePromotionAsyncTest : IntegrationTest // Francis: Je crois que il y a un erreur de JSONTranscript parce que request est corrupted rendu dans la méthode du controlleur
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
            return await _httpClient.PostAsJsonAsync("api/promotions", request);
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
                Duration = new Duration(TimeSpan.FromDays(30).ToDuration()),
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
                Duration = new Duration(TimeSpan.FromDays(30).ToDuration()),
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

        [Fact]
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

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var promotionRepository = scope.GetRequiredService<IPromotionRepository>();

                    var promotion = await promotionRepository.FindPromotionOrNullAsync(TestCode);

                    promotion.Should().NotBeNull();
                    promotion?.Amount.Should().Be(request.Currency.Amount);
                    promotion?.CurrencyType.Should().Be(request.Currency.Type);
                    promotion?.PromotionalCode.Should().BeSameAs(request.PromotionalCode);
                    promotion?.Duration.Should().Be(request.Duration.ToTimeSpan());
                    promotion?.ExpiredAt.Should().Be(request.ExpiredAt.ToDateTime());
                });
        }
    }
}
