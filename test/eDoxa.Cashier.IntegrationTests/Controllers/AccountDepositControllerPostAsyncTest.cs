// Filename: AccountDepositControllerPostAsyncTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Cashier.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountDepositControllerPostAsyncTest : IntegrationTest
    {
        public AccountDepositControllerPostAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency, decimal amount)
        {
            return await _httpClient.PostAsync($"api/account/deposit/{currency}", new JsonContent(amount));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = TestApi.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Currency.Token, 2500M);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = TestApi.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Currency.Money, Money.Fifty);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }
    }
}
