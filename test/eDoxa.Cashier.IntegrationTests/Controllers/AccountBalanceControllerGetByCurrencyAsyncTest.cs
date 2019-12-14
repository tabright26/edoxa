// Filename: AccountBalanceControllerGetByCurrencyAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountBalanceControllerGetByCurrencyAsyncTest : IntegrationTest
    {
        public AccountBalanceControllerGetByCurrencyAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper,
            async (httpClient, linkGenerator, values) =>
            {
                var path = linkGenerator.GetPathByName("Test", values);

                return await httpClient.GetAsync(path);
            })
        {
        }

        //private HttpClient _httpClient;

        //private async Task<HttpResponseMessage> ExecuteAsync(Currency currency)
        //{
        //    return await _httpClient.GetAsync($"api/balance/{currency}");
        //}

        [Fact]
        public async Task ShouldBeHttpStatusCodeInternalServerError()
        {
            var account = new Account(new UserId());

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, account.Id.ToString()));
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
            using var response = await ExecuteFuncAsync(
                factory.CreateClient(),
                server.Services.GetRequiredService<LinkGenerator>(),
                new
                {
                    currency = Currency.All
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var currency = Currency.Money;
            var account = new Account(new UserId());
            var balance = account.GetBalanceFor(currency);
            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, account.Id.ToString()));
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
            using var response = await ExecuteFuncAsync(
                factory.CreateClient(),
                server.Services.GetRequiredService<LinkGenerator>(),
                new
                {
                    currency
                });

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var balanceDto = await response.Content.ReadAsJsonAsync<BalanceDto>();

            balanceDto.Should().NotBeNull();
            balanceDto?.Currency.Should().Be(currency.ToEnum<CurrencyDto>());
            balanceDto?.Available.Should().Be(DecimalValue.FromDecimal(balance.Available));
            balanceDto?.Pending.Should().Be(DecimalValue.FromDecimal(balance.Pending));
        }
    }
}
