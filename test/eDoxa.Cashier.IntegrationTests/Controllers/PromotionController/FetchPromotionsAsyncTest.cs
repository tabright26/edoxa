// Filename: FetchPromotionsAsyncTest.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
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
    public sealed class FetchPromotionsAsyncTest : IntegrationTest
    {
        public FetchPromotionsAsyncTest(
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

        private HttpClient _httpClient;

        private static List<Promotion> GenerateValidPromotions()
        {
            return new List<Promotion>
            {
                new Promotion(
                    "code1",
                    new Money(50),
                    TimeSpan.FromDays(30),
                    new DateTimeProvider(DateTime.UtcNow.AddDays(30))),
                new Promotion(
                    "code2",
                    new Token(20),
                    TimeSpan.FromDays(30),
                    new DateTimeProvider(DateTime.UtcNow.AddDays(30))),
                new Promotion(
                    "code3",
                    new Money(20),
                    TimeSpan.FromDays(30),
                    new DateTimeProvider(DateTime.UtcNow.AddDays(30)))
            };
        }

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/promotions");
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
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IPromotionRepository>();
                    var promotions = GenerateValidPromotions();

                    foreach (var promotion in promotions)
                    {
                        repository.Create(promotion);
                    }

                    await repository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Francis: Ici, comment je devrais cast la reponse ?? Est-ce que je devrais utiliser un JsonMapper ? ou bien c'est juste pas necessaire
            response.Content.As<List<Promotion>>().Should().BeSameAs(GenerateValidPromotions());
        }
    }
}
