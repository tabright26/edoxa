// Filename: TransactionsControllerGetAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Responses;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class TransactionsControllerGetAsyncTest : IntegrationTest
    {
        public TransactionsControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency = null, TransactionType type = null, TransactionStatus status = null)
        {
            return await _httpClient.GetAsync($"api/transactions?currency={currency}&type={type}&status={status}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));
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
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(1);
            var account = accountFaker.FakeAccount();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));
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
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var transactions = await response.DeserializeAsync<TransactionResponse[]>();
            transactions.Should().HaveCount(account.Transactions.Count);
        }
    }
}
