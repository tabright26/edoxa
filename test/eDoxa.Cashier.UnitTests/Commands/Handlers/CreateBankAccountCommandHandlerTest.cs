// Filename: CreateBankAccountCommandHandlerTest.cs
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
using eDoxa.Cashier.Api.Application.Data.Fakers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.UnitTests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateBankAccountCommandHandlerTest
    {
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
            TestConstructor<CreateBankAccountCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IStripeService), typeof(IUserRepository))
                .WithClassName("CreateBankAccountCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateBankAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            user.RemoveBankAccount();

            _mockUserRepository.Setup(mock => mock.GetUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockUserRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateBankAccountCommandHandler(_mockHttpContextAccessor.Object, _mockStripeService.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(new CreateBankAccountCommand("qwe23rwr2r12rqwe123qwsda241qweasd"));

            // Assert
            _mockStripeService.Verify(
                mock => mock.CreateBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
