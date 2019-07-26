// Filename: CreateUserRequestHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.Application.Requests.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Requests.Handlers
{
    [TestClass]
    public sealed class CreateUserRequestHandlerTest
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
            TestConstructor<CreateUserRequestHandler>.ForParameters(typeof(IAccountRepository)).WithClassName("CreateUserRequestHandler").Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateUserRequest_ShouldBeCompletedTask()
        {
            // Arrange
            _mockAccountRepository.Setup(mock => mock.Create(It.IsAny<IAccount>())).Verifiable();
            _mockAccountRepository.Setup(mock => mock.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
            var handler = new CreateUserRequestHandler(_mockAccountRepository.Object);

            // Act
            await handler.HandleAsync(new CreateUserRequest(new UserId()));

            // Assert
            _mockAccountRepository.Verify(mock => mock.Create(It.IsAny<IAccount>()), Times.Once);
            _mockAccountRepository.Verify(mock => mock.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
