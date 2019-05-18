// Filename: StripeCardQueriesTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Functional;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Queries
{
    [TestClass]
    public sealed class StripeCardQueriesTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<IMapper> _mockMapper;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StripeCardQueries>.For(typeof(IStripeService), typeof(ICashierHttpContext), typeof(IMapper))
                .WithName("StripeCardQueries")
                .Assert();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldNotBeEmpty()
        {
            // Arrange
            _mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()))
                .ReturnsAsync(new List<Card> {new Card()})
                .Verifiable();

            _mockMapper.Setup(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>()))
                .Returns(new StripeCardListDTO {Items = new List<StripeCardDTO> {new StripeCardDTO()}}).Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockCashierHttpContext.Object, _mockMapper.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().NotBeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeEmpty()
        {
            // Arrange
            _mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()))
                .ReturnsAsync(new List<Card>())
                .Verifiable();

            _mockMapper.Setup(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>())).Returns(new StripeCardListDTO()).Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockCashierHttpContext.Object, _mockMapper.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().BeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCardAsync_ShouldNotBeEmpty()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockStripeService.Setup(mock => mock.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()))
                .ReturnsAsync(new Option<Card>(new Card()))
                .Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockCashierHttpContext.Object, _mockMapper.Object);

            // Act
            var result = await queries.GetCardAsync(cardId);

            result.Should().NotBeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCardAsync_ShouldBeEmpty()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockStripeService.Setup(mock => mock.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()))
                .ReturnsAsync(new Option<Card>())
                .Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockCashierHttpContext.Object, _mockMapper.Object);

            // Act
            var result = await queries.GetCardAsync(cardId);

            result.Should().BeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()), Times.Once);
        }
    }
}