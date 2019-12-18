//// Filename: RoleClaimsRemovedIntegrationEventHandlerTest.cs
//// Date Created: 2019-11-25
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Security.Claims;
//using System.Threading.Tasks;

//using eDoxa.Grpc.Protos.Identity.Dtos;
//using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
//using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
//using eDoxa.Identity.Domain.Services;
//using eDoxa.Identity.TestHelper;
//using eDoxa.Identity.TestHelper.Fixtures;

//using Microsoft.AspNetCore.Identity;

//using Moq;

//using Xunit;

//namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
//{
//    public sealed class RoleClaimsRemovedIntegrationEventHandlerTest : UnitTest
//    {
//        public RoleClaimsRemovedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
//        {
//        }

//        [Fact]
//        public async Task RoleClaimRemovedIntegrationEvent_ShouldBeCompletedTask()
//        {
//            // Arrange
//            var mockRoleManager = new Mock<IRoleService>();

//            mockRoleManager.Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

//            mockRoleManager.Setup(roleManager => roleManager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Role()).Verifiable();

//            mockRoleManager.Setup(roleManager => roleManager.RemoveClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()))
//                .ReturnsAsync(IdentityResult.Success)
//                .Verifiable();

//            var handler = new RoleClaimsRemovedIntegrationEventHandler(mockRoleManager.Object);

//            var integrationEvent = new RoleClaimsRemovedIntegrationEvent
//            {
//                RoleName = "role",
//                Claims =
//                {
//                    new RoleClaimDto
//                    {
//                        Type = "admin",
//                        Value = "allow"
//                    }
//                }
//            };

//            // Act
//            await handler.HandleAsync(integrationEvent);

//            // Assert
//            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
//            mockRoleManager.Verify(roleManager => roleManager.FindByNameAsync(It.IsAny<string>()), Times.Once);
//            mockRoleManager.Verify(roleManager => roleManager.RemoveClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()), Times.Once);
//        }
//    }
//}
