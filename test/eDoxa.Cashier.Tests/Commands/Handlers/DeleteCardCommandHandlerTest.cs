// Filename: DeleteCardCommandHandlerTest.cs
// Date Created: 2019-05-28
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
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DeleteCardCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
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
            ConstructorTests<DeleteCardCommandHandler>.For(typeof(IHttpContextAccessor), typeof(IStripeService), typeof(IUserRepository))
                .WithName("DeleteCardCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DeleteCardCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var card = FakeStripeFactory.CreateCard();

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            var handler = new DeleteCardCommandHandler(_mockHttpContextAccessor.Object, _mockStripeService.Object, _mockUserRepository.Object);

            // Act
            var result = await handler.HandleAsync(new DeleteCardCommand(new StripeCardId(card.Id)));

            // Assert
            result.Should().BeOfType<Either<ValidationResult, CommandResult>>();

            _mockStripeService.Verify(
                mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
