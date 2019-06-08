// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.UnitTests.Utilities.Fakes;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.UnitTests.Utilities;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<UserCreatedIntegrationEventHandler>.ForParameters(typeof(IMediator)).WithClassName("UserCreatedIntegrationEventHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_InitializeServiceCommand_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = FakeCashierFactory.CreateUserId();

            var person = StripeBuilder.CreatePerson();

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = userId.ToGuid(),
                Email = person.Email,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Year = person.Dob.Year.HasValue ? (int) person.Dob.Year.Value : 1970,
                Month = person.Dob.Month.HasValue ? (int) person.Dob.Month.Value : 1,
                Day = person.Dob.Day.HasValue ? (int) person.Dob.Day.Value : 1
            };

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.Handle(integrationEvent);

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
