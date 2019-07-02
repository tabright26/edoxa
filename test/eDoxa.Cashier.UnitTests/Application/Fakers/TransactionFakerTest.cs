// Filename: TransactionFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Fakers
{
    [TestClass]
    public sealed class TransactionFakerTest
    {
        [TestMethod]
        public void FakePositiveTransactions_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var transactions = transactionFaker.Generate(10, TransactionFaker.PositiveTransaction);

                    Console.WriteLine(transactions.DumbAsJson());

                    transactions.Should().HaveCount(10);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakePositiveTransaction_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var transaction = transactionFaker.Generate(TransactionFaker.PositiveTransaction);

                    Console.WriteLine(transaction.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeNegativeTransactions_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var transactions = transactionFaker.Generate(10, TransactionFaker.NegativeTransaction);

                    Console.WriteLine(transactions.DumbAsJson());

                    transactions.Should().HaveCount(10);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeNegativeTransaction_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var transaction = transactionFaker.Generate(TransactionFaker.NegativeTransaction);

                    Console.WriteLine(transaction.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
