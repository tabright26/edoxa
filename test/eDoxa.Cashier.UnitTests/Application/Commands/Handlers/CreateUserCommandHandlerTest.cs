// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-06-01
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

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Api.Application.Data.Fakers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.UnitTests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
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
            TestConstructor<CreateUserCommandHandler>.ForParameters(typeof(IStripeService), typeof(IUserRepository)).WithClassName("CreateUserCommandHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_InitializeServiceCommand_ShouldBeCompletedTask()
        {
            // Arrange
            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            var personFaker = new PersonFaker();

            var person = personFaker.FakePerson();

            _mockUserRepository.Setup(mock => mock.Create(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            _mockUserRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateUserCommandHandler(_mockStripeService.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(
                new CreateUserCommand(
                    user.Id,
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
