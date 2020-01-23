// Filename: PaymentGrpcServiceTest.cs
// Date Created: 2020-01-17
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Responses;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

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

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email))
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestContainer<ContainerBuilder>(
                            container =>
                            {
                                var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

                                mockServiceBusPublisher.Setup(x => x.PublishAsync(It.IsAny<UserDepositSucceededIntegrationEvent>()))
                                    .Returns(Task.CompletedTask);

                                container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>();

                                var mockStripeCustomerService = new Mock<IStripeCustomerService>();

                                mockStripeCustomerService.Setup(x => x.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId");

                                mockStripeCustomerService.Setup(x => x.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(true);

                                container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>();

                                var mockStripeInvoiceService = new Mock<IStripeInvoiceService>();

                                mockStripeInvoiceService.Setup(
                                        x => x.CreateInvoiceAsync(
                                            It.IsAny<string>(),
                                            It.IsAny<TransactionId>(),
                                            It.IsAny<long>(),
                                            It.IsAny<string>()))
                                    .ReturnsAsync(new Invoice());

                                container.RegisterInstance(mockStripeInvoiceService.Object).As<IStripeInvoiceService>();
                            });
                    });

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var customerService = scope.GetRequiredService<IStripeCustomerService>();

                    await customerService.CreateCustomerAsync(userId, email);

                    var customerId = await customerService.GetCustomerIdAsync(userId);

                    await customerService.SetDefaultPaymentMethodAsync(customerId, "testId");
                });

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Amount = 20,
                    Currency = EnumCurrency.Money,
                    Description = "Test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Pending,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Deposit
                }
            };

            // Act
            var response = await client.DepositAsync(request);

            //Assert
            response.Should().BeOfType<DepositResponse>();
        }

        [Fact]
        public void Deposit_ShouldThrowRpcExceptionNoDefaultPayment()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email))
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestContainer<ContainerBuilder>(
                            container =>
                            {
                                var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

                                mockServiceBusPublisher.Setup(x => x.PublishAsync(It.IsAny<UserDepositSucceededIntegrationEvent>()))
                                    .Returns(Task.CompletedTask);

                                container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>();

                                var mockStripeCustomerService = new Mock<IStripeCustomerService>();

                                mockStripeCustomerService.Setup(x => x.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId");

                                mockStripeCustomerService.Setup(x => x.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(false);

                                container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>();
                            });
                    });

            host.Server.CleanupDbContext();

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

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

            // Act 
            var func = new Func<Task>(async () => await client.DepositAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void Deposit_ShouldThrowRpcExceptionWithInternalStatus()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

            host.Server.CleanupDbContext();

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

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

            // Act
            var func = new Func<Task>(async () => await client.DepositAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task Withdrawal_ShouldBeOfTypeWithdrawalResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email))
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestContainer<ContainerBuilder>(
                            container =>
                            {
                                var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

                                mockServiceBusPublisher.Setup(x => x.PublishAsync(It.IsAny<UserWithdrawalSucceededIntegrationEvent>()))
                                    .Returns(Task.CompletedTask);

                                container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>();

                                var mockStripeTransferService = new Mock<IStripeTransferService>();

                                mockStripeTransferService.Setup(
                                        x => x.CreateTransferAsync(
                                            It.IsAny<string>(),
                                            It.IsAny<TransactionId>(),
                                            It.IsAny<long>(),
                                            It.IsAny<string>()))
                                    .ReturnsAsync(new Transfer());

                                container.RegisterInstance(mockStripeTransferService.Object).As<IStripeTransferService>();

                                var mockStripeAccountService = new Mock<IStripeAccountService>();

                                mockStripeAccountService.Setup(x => x.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId");

                                mockStripeAccountService.Setup(x => x.HasAccountVerifiedAsync(It.IsAny<string>())).ReturnsAsync(true);

                                container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>();
                            });
                    });

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

            // Act
            var response = await client.WithdrawalAsync(request);

            //Assert
            response.Should().BeOfType<WithdrawalResponse>();
        }

        [Fact]
        public void Withdrawal_ShouldThrowRpcExceptionAccountVerificationNeeded()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

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

        [Fact]
        public void Withdrawal_ShouldThrowRpcExceptionWithInternalStatus()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

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

            // Act 
            var func = new Func<Task>(async () => await client.WithdrawalAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }
    }
}
