// Filename: CreateCardCommandHandlerTest.cs
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
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class CreateCardCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private MockStripeService _mockStripeService;
        private Mock<IUserQuery> _mockUserQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new MockStripeService();
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockUserQuery = new Mock<IUserQuery>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CreateCardCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IStripeService), typeof(IUserQuery))
                .WithClassName("CreateCardCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateCardCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserQuery.Setup(userQuery => userQuery.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            var handler = new CreateCardCommandHandler(_mockHttpContextAccessor.Object, _mockStripeService.Object, _mockUserQuery.Object);

            // Act
            await handler.HandleAsync(new CreateCardCommand(card.Id));

            // Assert
            _mockStripeService.Verify(
                stripeService => stripeService.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
