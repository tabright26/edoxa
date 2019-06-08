// Filename: StripeCardQueriesTest.cs
// Date Created: 2019-06-01
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

using eDoxa.Cashier.Api.Application.Queries;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.UnitTests.Utilities.Fakes;
using eDoxa.Cashier.UnitTests.Utilities.Mocks.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.UnitTests.Queries
{
    [TestClass]
    public sealed class StripeCardQueriesTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMapper> _mockMapper;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockMapper = new Mock<IMapper>();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CardQuery>.ForParameters(typeof(IStripeService), typeof(IHttpContextAccessor), typeof(IMapper), typeof(IUserRepository))
                .WithClassName("CardQuery")
                .Assert();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldNotBeEmpty()
        {
            // Arrange
            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()))
                .ReturnsAsync(
                    new List<Card>
                    {
                        new Card()
                    }
                )
                .Verifiable();

            _mockMapper.Setup(mock => mock.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()))
                .Returns(
                    new List<CardViewModel>
                    {
                        new CardViewModel()
                    }
                )
                .Verifiable();

            var queries = new CardQuery(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserRepository.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().NotBeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mock => mock.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeEmpty()
        {
            // Arrange
            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(new List<Card>()).Verifiable();

            _mockMapper.Setup(mock => mock.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()))
                .Returns(new List<CardViewModel>())
                .Verifiable();

            var queries = new CardQuery(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserRepository.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().BeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mock => mock.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()), Times.Once);
        }
    }
}
