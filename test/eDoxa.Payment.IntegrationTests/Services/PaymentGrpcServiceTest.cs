// Filename: PaymentGrpcServiceTest.cs
// Date Created: 2020-01-13
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Responses;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using IdentityModel;

using Xunit;

namespace eDoxa.Payment.IntegrationTests.Services
{
    public sealed class PaymentGrpcServiceTest : IntegrationTest
    {
        public PaymentGrpcServiceTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        [Fact]
        public async Task Deposit_ShouldBeOfTypeDepositResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var customerService = scope.GetRequiredService<IStripeCustomerService>();

                    await customerService.CreateCustomerAsync(userId, email);

                    var customerId = await customerService.GetCustomerIdAsync(userId);

                    await customerService.SetDefaultPaymentMethodAsync(customerId, "testId");
                });

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Deposit
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act
            var response = await client.DepositAsync(request);

            //Assert
            response.Should().BeOfType<DepositResponse>();
        }

        [Fact]
        public async Task Deposit_ShouldThrowRpcExceptionNoDefaultPayment()
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
                    var customerService = scope.GetRequiredService<IStripeCustomerService>();

                    await customerService.CreateCustomerAsync(userId, email);
                });

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Deposit
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.DepositAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void Deposit_ShouldThrowRpcExceptionWithInternalStatus()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Deposit
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.DepositAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task Withdrawal_ShouldBeOfTypeWithdrawalResponse()
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
                    var customerService = scope.GetRequiredService<IStripeCustomerService>();
                    var accountService = scope.GetRequiredService<IStripeAccountService>();

                    await customerService.CreateCustomerAsync(userId, email);
                    var customerId = await customerService.GetCustomerIdAsync(userId);

                    await accountService.CreateAccountAsync(
                        userId,
                        email,
                        Country.Canada,
                        customerId);
                });

            var request = new WithdrawalRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Withdrawal
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act
            var response = await client.WithdrawalAsync(request);

            //Assert
            response.Should().BeOfType<WithdrawalResponse>();
        }

        [Fact]
        public async Task Withdrawal_ShouldThrowRpcExceptionAccountVerificationNeeded()
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
                    var customerService = scope.GetRequiredService<IStripeCustomerService>();
                    var accountService = scope.GetRequiredService<IStripeAccountService>();

                    await customerService.CreateCustomerAsync(userId, email);
                    var customerId = await customerService.GetCustomerIdAsync(userId);

                    await accountService.CreateAccountAsync(
                        userId,
                        email,
                        Country.Canada,
                        customerId);
                });

            var request = new WithdrawalRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Withdrawal
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.WithdrawalAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void Withdrawal_ShouldThrowRpcExceptionWithInternalStatus()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new WithdrawalRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Withdrawal
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.WithdrawalAsync(request));
            func.Should().Throw<RpcException>();
        }
    }
}
