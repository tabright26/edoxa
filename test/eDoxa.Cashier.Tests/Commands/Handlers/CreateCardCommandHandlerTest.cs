// Filename: CreateCardCommandHandlerTest.cs
// Date Created: 2019-05-29
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
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Tests.Utilities.Fakes;
using eDoxa.Cashier.Tests.Utilities.Mocks.Extensions;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;
using eDoxa.Stripe.Tests.Utilities;
using eDoxa.Testing.MSTest.Constructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateCardCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateCardCommandHandler>.For(typeof(IHttpContextAccessor), typeof(IStripeService), typeof(IUserRepository))
                .WithName("CreateCardCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateCardCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var card = StripeBuilder.CreateCard();

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            var handler = new CreateCardCommandHandler(_mockHttpContextAccessor.Object, _mockStripeService.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(new CreateCardCommand(card.Id));

            // Assert
            _mockStripeService.Verify(mock => mock.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
