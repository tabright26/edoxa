// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Application.IntegrationEvents.Handlers;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Testing.MSTest;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserCreatedIntegrationEventHandler>.For(typeof(IMediator)).WithName("UserCreatedIntegrationEventHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_InitializeServiceCommand_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = FakeCashierFactory.CreateUserId();

            var person = FakeStripeFactory.CreatePerson();

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

            _mockMediator.Setup(mock => mock.Send(It.IsAny<InitializeServiceCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.Handle(integrationEvent);

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<InitializeServiceCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
