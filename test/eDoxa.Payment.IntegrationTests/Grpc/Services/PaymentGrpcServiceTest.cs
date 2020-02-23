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
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.Stripe.Services.Abstractions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Payment.IntegrationTests.Grpc.Services
{
    public sealed class PaymentGrpcServiceTest : IntegrationTest
    {
        public PaymentGrpcServiceTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        //[Fact] // FRANCIS: TODO
        //public async Task Deposit_ShouldBeOfTypeDepositResponse()
        //{
        //    // Arrange
        //    var userId = new UserId();
        //    const string email = "test@edoxa.gg";

        //    var host = TestHost.WithClaimsFromBearerAuthentication(
        //            new Claim(JwtClaimTypes.Subject, userId.ToString()),
        //            new Claim(JwtClaimTypes.Email, email),
        //            new Claim(CustomClaimTypes.StripeCustomer, "customerId"));

        //    var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

        //    var request = new CreateStripePaymentIntentRequest
        //    {
        //        Transaction = new TransactionDto
        //        {
        //            Currency = new CurrencyDto
        //            {
        //                Type = EnumCurrencyType.Money,
        //                Amount = 20
        //            },
        //            Description = "Test",
        //            Id = new TransactionId(),
        //            Status = EnumTransactionStatus.Pending,
        //            Timestamp = DateTime.UtcNow.ToTimestamp(),
        //            Type = EnumTransactionType.Deposit
        //        }
        //    };

        //    // Act
        //    var response = await client.CreateStripePaymentIntentAsync(request);

        //    //Assert
        //    response.Should().BeOfType<CreateStripePaymentIntentRequest>();
        //}

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

            var request = new CreateStripePaymentIntentRequest
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
            var func = new Func<Task>(async () => await client.CreateStripePaymentIntentAsync(request));

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

            var request = new CreateStripePaymentIntentRequest
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
            var func = new Func<Task>(async () => await client.CreateStripePaymentIntentAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void Withdraw_ShouldThrowRpcExceptionAccountVerificationNeeded()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

            var request = new CreatePaypalPayoutRequest
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
                    Type = EnumTransactionType.Withdraw
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.CreatePaypalPayoutAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void Withdraw_ShouldThrowRpcExceptionWithInternalStatus()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email));

            var request = new CreatePaypalPayoutRequest
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
                    Type = EnumTransactionType.Withdraw
                }
            };

            var client = new PaymentService.PaymentServiceClient(host.CreateChannel());

            // Act 
            var func = new Func<Task>(async () => await client.CreatePaypalPayoutAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }
    }
}
