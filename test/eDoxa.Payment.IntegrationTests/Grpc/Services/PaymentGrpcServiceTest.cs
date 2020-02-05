// Filename: PaymentGrpcServiceTest.cs
// Date Created: 2020-01-28
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
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
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

namespace eDoxa.Payment.IntegrationTests.Grpc.Services
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
                                TestMock.ServiceBusPublisher.Setup(x => x.PublishAsync(It.IsAny<UserDepositSucceededIntegrationEvent>()))
                                    .Returns(Task.CompletedTask);

                                container.RegisterInstance(TestMock.ServiceBusPublisher.Object).As<IServiceBusPublisher>();

                                TestMock.StripeCustomerService.Setup(x => x.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(true);

                                container.RegisterInstance(TestMock.StripeCustomerService.Object).As<IStripeCustomerService>();

                                TestMock.StripeInvoiceService.Setup(
                                        x => x.CreateInvoiceAsync(
                                            It.IsAny<string>(),
                                            It.IsAny<TransactionId>(),
                                            It.IsAny<long>(),
                                            It.IsAny<string>()))
                                    .ReturnsAsync(new Invoice());

                                container.RegisterInstance(TestMock.StripeInvoiceService.Object).As<IStripeInvoiceService>();
                            });
                    });

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = 20
                    },
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
                                TestMock.ServiceBusPublisher.Setup(x => x.PublishAsync(It.IsAny<UserDepositSucceededIntegrationEvent>()))
                                    .Returns(Task.CompletedTask);

                                container.RegisterInstance(TestMock.ServiceBusPublisher.Object).As<IServiceBusPublisher>();

                                TestMock.StripeCustomerService.Setup(x => x.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(false);

                                container.RegisterInstance(TestMock.StripeCustomerService.Object).As<IStripeCustomerService>();
                            });
                    });

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = 20
                    },
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

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            var request = new DepositRequest
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = 20
                    },
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
        public void Withdrawal_ShouldThrowRpcExceptionAccountVerificationNeeded()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

            var request = new WithdrawalRequest
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = 20
                    },
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

            var request = new WithdrawalRequest
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = 20
                    },
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
