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

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class StripeCardQueriesTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IMapper> _mockMapper;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserQuery> _mockUserQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockStripeService = new Mock<IStripeService>();
            _mockMapper = new Mock<IMapper>();
            _mockUserQuery = new Mock<IUserQuery>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<CardQuery>.ForParameters(typeof(IStripeService), typeof(IHttpContextAccessor), typeof(IMapper), typeof(IUserQuery))
                .WithClassName("CardQuery")
                .Assert();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldNotBeEmpty()
        {
            // Arrange
            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserQuery.Setup(userQuery => userQuery.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(stripeService => stripeService.GetCardsAsync(It.IsAny<StripeCustomerId>()))
                .ReturnsAsync(
                    new List<Card>
                    {
                        new Card()
                    }
                )
                .Verifiable();

            _mockMapper.Setup(mapper => mapper.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()))
                .Returns(
                    new List<CardViewModel>
                    {
                        new CardViewModel()
                    }
                )
                .Verifiable();

            var queries = new CardQuery(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserQuery.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().NotBeEmpty();

            // Assert
            _mockStripeService.Verify(stripeService => stripeService.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mapper => mapper.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeEmpty()
        {
            // Arrange
            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserQuery.Setup(userQuery => userQuery.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(stripeService => stripeService.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(new List<Card>()).Verifiable();

            _mockMapper.Setup(mapper => mapper.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()))
                .Returns(new List<CardViewModel>())
                .Verifiable();

            var cardQuery = new CardQuery(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserQuery.Object);

            // Act
            var result = await cardQuery.GetCardsAsync();

            result.Should().BeEmpty();

            // Assert
            _mockStripeService.Verify(stripeService => stripeService.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mapper => mapper.Map<IReadOnlyCollection<CardViewModel>>(It.IsAny<IReadOnlyCollection<Card>>()), Times.Once);
        }
    }
}
