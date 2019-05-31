// Filename: StripeCardQueriesTest.cs
// Date Created: 2019-05-29
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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.Tests.Utilities.Fakes;
using eDoxa.Cashier.Tests.Utilities.Mocks.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Tests.Queries
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
            ConstructorTests<StripeCardQueries>.For(typeof(IStripeService), typeof(IHttpContextAccessor), typeof(IMapper), typeof(IUserRepository))
                .WithName("StripeCardQueries")
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

            _mockMapper.Setup(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>()))
                .Returns(
                    new StripeCardListDTO
                    {
                        Items = new List<StripeCardDTO>
                        {
                            new StripeCardDTO()
                        }
                    }
                )
                .Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserRepository.Object);

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
            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(new List<Card>()).Verifiable();

            _mockMapper.Setup(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>())).Returns(new StripeCardListDTO()).Verifiable();

            var queries = new StripeCardQueries(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockMapper.Object, _mockUserRepository.Object);

            // Act
            var result = await queries.GetCardsAsync();

            result.Should().BeEmpty();

            // Assert
            _mockStripeService.Verify(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()), Times.Once);

            _mockMapper.Verify(mock => mock.Map<StripeCardListDTO>(It.IsAny<IEnumerable<Card>>()), Times.Once);
        }
    }
}
