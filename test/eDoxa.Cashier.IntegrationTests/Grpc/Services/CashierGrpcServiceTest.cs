// Filename: CashierGrpcServiceTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Grpc.Core;

using IdentityModel;

using Microsoft.AspNetCore.Routing;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Grpc.Services
{
    public sealed class CashierGrpcServiceTest : IntegrationTest
    {
        public CashierGrpcServiceTest(
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

        [Fact]
        public async Task CreateChallengePayout_ShouldBeOfTypeFindChallengePayoutResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new CreateChallengePayoutRequest
            {
                ChallengeId = new ChallengeId(),
                EntryFee = new EntryFeeDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money
                },
                PayoutEntries = 10
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act
            var response = await client.CreateChallengePayoutAsync(request);

            //Assert
            response.Should().BeOfType<CreateChallengePayoutResponse>();
        }

        [Fact]
        public void CreateChallengePayout_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            var request = new CreateChallengePayoutRequest
            {
                ChallengeId = new ChallengeId(),
                EntryFee = new EntryFeeDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money
                }
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.CreateChallengePayoutAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task CreateTransaction_ShouldBeOfTypeCreateTransactionResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var accountService = scope.GetRequiredService<IAccountService>();

                    await accountService.CreateAccountAsync(userId);
                });

            var request = new CreateTransactionRequest
            {
                Bundle = 0,
                Custom = new CreateTransactionRequest.Types.CustomTransaction
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Type = EnumTransactionType.Deposit
                }
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act
            var response = await client.CreateTransactionAsync(request);

            //Assert
            response.Should().BeOfType<CreateTransactionResponse>();
        }

        [Fact]
        public void CreateTransaction_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            var request = new CreateTransactionRequest
            {
                Bundle = 0,
                Custom = new CreateTransactionRequest.Types.CustomTransaction
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Type = EnumTransactionType.Charge
                }
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.CreateTransactionAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void CreateTransaction_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            var request = new CreateTransactionRequest
            {
                Bundle = 0,
                Custom = new CreateTransactionRequest.Types.CustomTransaction
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Type = EnumTransactionType.Deposit
                }
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.CreateTransactionAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task FetchChallengePayouts_ShouldBeOfTypeFetchChallengePayoutsResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var accountService = scope.GetRequiredService<IAccountService>();

                    await accountService.CreateAccountAsync(userId);
                });

            var request = new FetchChallengePayoutsRequest();

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act
            var response = await client.FetchChallengePayoutsAsync(request);

            //Assert
            response.Should().BeOfType<FetchChallengePayoutsResponse>();
        }

        [Fact]
        public async Task FindChallengePayout_ShouldBeOfTypeFindChallengePayoutResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var challengeId = new ChallengeId();

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};

            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var accountService = scope.GetRequiredService<IAccountService>();
                    var challengeService = scope.GetRequiredService<IChallengeService>();

                    await accountService.CreateAccountAsync(userId);
                    await challengeService.CreateChallengeAsync(challengeId, ChallengePayoutEntries.Fifteen, new EntryFee(20, Currency.Money));
                });

            var request = new FindChallengePayoutRequest
            {
                ChallengeId = challengeId
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act
            var response = await client.FindChallengePayoutAsync(request);

            //Assert
            response.Should().BeOfType<FindChallengePayoutResponse>();
        }

        [Fact]
        public async Task FindChallengePayout_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var accountService = scope.GetRequiredService<IAccountService>();
                    await accountService.CreateAccountAsync(userId);
                });

            var request = new FindChallengePayoutRequest
            {
                ChallengeId = new ChallengeId()
            };

            var client = new CashierService.CashierServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.FindChallengePayoutAsync(request));
            func.Should().Throw<RpcException>();
        }
    }
}
