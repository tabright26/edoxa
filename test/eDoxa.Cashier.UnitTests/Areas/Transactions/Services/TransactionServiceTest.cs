// Filename: TransactionServiceTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

namespace eDoxa.Cashier.UnitTests.Areas.Transactions.Services
{
    public sealed class TransactionServiceTest : UnitTest
    {
        public TransactionServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task FindTransactionAsync_WithId_ShouldBeOfTypeTransaction()
        //{
        //    // Arrange
        //    var mockTransactionRepository = new Mock<ITransactionRepository>();

        //    var transaction = new Transaction(
        //        new Money(50),
        //        new TransactionDescription("test"),
        //        TransactionType.Deposit,
        //        new UtcNowDateTimeProvider());

        //    mockTransactionRepository.Setup(repository => repository.FindTransactionAsync(It.IsAny<TransactionId>())).ReturnsAsync(transaction).Verifiable();

        //    var service = new TransactionService(mockTransactionRepository.Object);

        //    // Act
        //    var result = await service.FindTransactionAsync(new TransactionId());

        //    // Assert
        //    result.Should().BeOfType<Transaction>();
        //    mockTransactionRepository.Verify(repository => repository.FindTransactionAsync(It.IsAny<TransactionId>()), Times.Once);
        //}

        //[Fact]
        //public async Task FindTransactionAsync_WithMetaData_ShouldBeOfTypeTransaction()
        //{
        //    // Arrange
        //    var mockTransactionRepository = new Mock<ITransactionRepository>();

        //    var transaction = new Transaction(
        //        new Money(50),
        //        new TransactionDescription("test"),
        //        TransactionType.Deposit,
        //        new UtcNowDateTimeProvider());

        //    mockTransactionRepository.Setup(repository => repository.FindTransactionAsync(It.IsAny<TransactionMetadata>()))
        //        .ReturnsAsync(transaction)
        //        .Verifiable();

        //    var service = new TransactionService(mockTransactionRepository.Object);

        //    // Act
        //    var result = await service.FindTransactionAsync(new TransactionMetadata());

        //    // Assert
        //    result.Should().BeOfType<Transaction>();
        //    mockTransactionRepository.Verify(repository => repository.FindTransactionAsync(It.IsAny<TransactionMetadata>()), Times.Once);
        //}

        //[Fact]
        //public async Task MarkTransactionAsCanceledAsync()
        //{
        //    // Arrange
        //    var mockTransactionRepository = new Mock<ITransactionRepository>();

        //    var transaction = new Transaction(
        //        new Money(50),
        //        new TransactionDescription("test"),
        //        TransactionType.Deposit,
        //        new UtcNowDateTimeProvider());

        //    mockTransactionRepository.Setup(repository => repository.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

        //    var service = new TransactionService(mockTransactionRepository.Object);

        //    // Act
        //    await service.MarkTransactionAsCanceledAsync(transaction);

        //    // Assert
        //    mockTransactionRepository.Verify(repository => repository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task MarkTransactionAsFailedAsync()
        //{
        //    // Arrange
        //    var mockTransactionRepository = new Mock<ITransactionRepository>();

        //    var transaction = new Transaction(
        //        new Money(50),
        //        new TransactionDescription("test"),
        //        TransactionType.Deposit,
        //        new UtcNowDateTimeProvider());

        //    mockTransactionRepository.Setup(repository => repository.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

        //    var service = new TransactionService(mockTransactionRepository.Object);

        //    // Act
        //    await service.MarkTransactionAsFailedAsync(transaction);

        //    // Assert
        //    mockTransactionRepository.Verify(repository => repository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task MarkTransactionAsSuccededAsync()
        //{
        //    // Arrange
        //    var mockTransactionRepository = new Mock<ITransactionRepository>();

        //    var transaction = new Transaction(
        //        new Money(50),
        //        new TransactionDescription("test"),
        //        TransactionType.Deposit,
        //        new UtcNowDateTimeProvider());

        //    mockTransactionRepository.Setup(repository => repository.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

        //    var service = new TransactionService(mockTransactionRepository.Object);

        //    // Act
        //    await service.MarkTransactionAsSuccededAsync(transaction);

        //    // Assert
        //    mockTransactionRepository.Verify(repository => repository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}
    }
}
