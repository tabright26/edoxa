// Filename: AccountTokenControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

//using System.Threading;
//using System.Threading.Tasks;

//using eDoxa.Cashier.Api.Controllers;
//using eDoxa.Cashier.Application.Commands;
//using eDoxa.Cashier.Domain.AggregateModels;
//using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
//using eDoxa.Testing.MSTest;

//using FluentAssertions;

//using FluentValidation.Results;

//using MediatR;

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Moq;

//namespace eDoxa.Cashier.Tests.Controllers
//{
//    [TestClass]
//    public sealed class AccountTokenControllerTest
//    {
//        private Mock<IMediator> _mockMediator;

//        [TestInitialize]
//        public void TestInitialize()
//        {
//            _mockMediator = new Mock<IMediator>();
//        }

//        [TestMethod]
//        public void Constructor_Tests()
//        {
//            ConstructorTests<AccountTokenController>.For(typeof(IMediator))
//                .WithName("AccountTokenController")
//                .WithAttributes(
//                    typeof(AuthorizeAttribute),
//                    typeof(ApiControllerAttribute),
//                    typeof(ApiVersionAttribute),
//                    typeof(ProducesAttribute),
//                    typeof(RouteAttribute),
//                    typeof(ApiExplorerSettingsAttribute)
//                )
//                .Assert();
//        }

//        [TestMethod]
//        public async Task DepositTokenAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            _mockMediator.Setup(mock => mock.Send(It.IsAny<DepositTokenCommand>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(TransactionStatus.Completed)
//                .Verifiable();

//            var controller = new AccountTokenController(_mockMediator.Object);

//            // Act
//            var result = await controller.DepositTokenAsync(new DepositTokenCommand(TokenDepositBundleType.FiftyThousand));

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();

//            _mockMediator.Verify(mock => mock.Send(It.IsAny<DepositTokenCommand>(), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [TestMethod]
//        public async Task DepositTokenAsync_ShouldBeOfTypeBadRequestObjectResult()
//        {
//            // Arrange
//            _mockMediator.Setup(mock => mock.Send(It.IsAny<DepositTokenCommand>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(new ValidationResult())
//                .Verifiable();

//            var controller = new AccountTokenController(_mockMediator.Object);

//            // Act
//            var result = await controller.DepositTokenAsync(new DepositTokenCommand(TokenDepositBundleType.FiftyThousand));

//            // Assert
//            result.Should().BeOfType<BadRequestObjectResult>();

//            _mockMediator.Verify(mock => mock.Send(It.IsAny<DepositTokenCommand>(), It.IsAny<CancellationToken>()), Times.Once);
//        }
//    }
//}


