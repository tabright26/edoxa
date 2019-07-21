// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        private Mock<IAccountRepository> _mockAccountRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CreateUserCommandHandler>.ForParameters(typeof(IAccountRepository)).WithClassName("CreateUserCommandHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateUserCommand_ShouldBeCompletedTask()
        {
            // Arrange
            _mockAccountRepository.Setup(mock => mock.Create(It.IsAny<IAccount>())).Verifiable();
            _mockAccountRepository.Setup(mock => mock.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
            var handler = new CreateUserCommandHandler(_mockAccountRepository.Object);

            // Act
            await handler.HandleAsync(new CreateUserCommand(new UserId()));

            // Assert
            _mockAccountRepository.Verify(mock => mock.Create(It.IsAny<IAccount>()), Times.Once);
            _mockAccountRepository.Verify(mock => mock.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
