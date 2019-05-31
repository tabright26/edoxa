// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Tests.Utilities.Fakes;
using eDoxa.Cashier.Tests.Utilities.Mocks.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;
using eDoxa.Stripe.Tests.Utilities;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateUserCommandHandler>.For(typeof(IStripeService), typeof(IUserRepository)).WithName("CreateUserCommandHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_InitializeServiceCommand_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = FakeCashierFactory.CreateUserId();

            var person = FakeStripeFactory.CreatePerson();

            _mockUserRepository.Setup(mock => mock.Create(It.IsAny<UserId>(), It.IsAny<StripeConnectAccountId>(), It.IsAny<StripeCustomerId>())).Verifiable();

            _mockUserRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateUserCommandHandler(_mockStripeService.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(
                new CreateUserCommand(
                    userId,
                    person.Email,
                    person.FirstName,
                    person.LastName,
                    person.Dob.Year.HasValue ? (int) person.Dob.Year.Value : 1970,
                    person.Dob.Month.HasValue ? (int) person.Dob.Month.Value : 1,
                    person.Dob.Day.HasValue ? (int) person.Dob.Day.Value : 1
                )
            );

            // Assert
            _mockStripeService.Verify(
                mock => mock.CreateCustomerAsync(It.IsAny<Guid>(), It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
