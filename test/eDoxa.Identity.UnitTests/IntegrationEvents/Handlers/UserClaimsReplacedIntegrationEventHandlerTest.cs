//// Filename: UserClaimsReplacedIntegrationEventHandlerTest.cs
//// Date Created: 2019-11-25
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;

//using eDoxa.Grpc.Protos.Identity.Dtos;
//using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
//using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
//using eDoxa.Identity.Domain.Services;
//using eDoxa.Identity.TestHelper;
//using eDoxa.Identity.TestHelper.Fixtures;

//using Microsoft.AspNetCore.Identity;

//using Moq;

//using Xunit;

//namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
//{
//    public sealed class UserClaimsReplacedIntegrationEventHandlerTest : UnitTest
//    {
//        public UserClaimsReplacedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
//        {
//        }

//        [Fact]
//        public async Task UserClaimsReplacedIntegrationEvent_ShouldBeCompletedTask()
//        {
//            // Arrange
//            var mockUserManager = new Mock<IUserService>();

//            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

//            mockUserManager.Setup(roleManager => roleManager.ReplaceClaimAsync(It.IsAny<User>(), It.IsAny<Claim>(), It.IsAny<Claim>()))
//                .ReturnsAsync(IdentityResult.Success)
//                .Verifiable();

//            var handler = new UserClaimsReplacedIntegrationEventHandler(mockUserManager.Object);

//            var integrationEvent = new UserClaimsReplacedIntegrationEvent
//            {
//                UserId = Guid.NewGuid().ToString(),
//                ClaimCount = 1,
//                Claims =
//                {
//                    new UserClaimDto
//                    {
//                        Type = "role",
//                        Value = "admin"
//                    }
//                },
//                NewClaims =
//                {
//                    new UserClaimDto
//                    {
//                        Type = "role",
//                        Value = "user"
//                    }
//                }
//            };

//            // Act
//            await handler.HandleAsync(integrationEvent);

//            // Assert
//            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
//            mockUserManager.Verify(roleManager => roleManager.ReplaceClaimAsync(It.IsAny<User>(), It.IsAny<Claim>(), It.IsAny<Claim>()), Times.Once);
//        }
//    }
//}
