// Filename: DeleteCardCommandHandlerTest.cs
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
    public sealed class DeleteCardCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private MockStripeService _mockStripeService;
        private Mock<IUserQuery> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new MockStripeService();
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockUserRepository = new Mock<IUserQuery>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DeleteCardCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IStripeService), typeof(IUserQuery))
                .WithClassName("DeleteCardCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DeleteCardCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserRepository.Setup(userQuery => userQuery.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            var handler = new DeleteCardCommandHandler(_mockHttpContextAccessor.Object, _mockStripeService.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(new DeleteCardCommand(new StripeCardId(card.Id)));

            // Assert
            _mockStripeService.Verify(
                mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
