﻿// Filename: FindUserBalanceAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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

namespace eDoxa.Cashier.IntegrationTests.Controllers.BalanceController
{
    public sealed class FindUserBalanceAsyncTest : IntegrationTest
    {
        public FindUserBalanceAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
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

        [Fact]
        public async Task ShouldBeHttpStatusCodeInternalServerError()
        {
            var account = new Account(new UserId());

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id.ToString()));
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
                    currencyType = CurrencyType.All
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var currencyType = CurrencyType.Money;
            var account = new Account(new UserId());
            var balance = account.GetBalanceFor(currencyType);
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id.ToString()));
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
                    currencyType
                });

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var balanceDto = await response.Content.ReadAsJsonAsync<BalanceDto>();

            balanceDto.Should().NotBeNull();
            balanceDto?.CurrencyType.Should().Be(currencyType.ToEnum<EnumCurrencyType>());
            balanceDto?.Available.Should().Be(DecimalValue.FromDecimal(balance.Available));
            balanceDto?.Pending.Should().Be(DecimalValue.FromDecimal(balance.Pending));
        }
    }
}
