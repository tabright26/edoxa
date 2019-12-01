//// Filename: TournamentGameMatchesControllerTest.cs
//// Date Created: 2019-11-01
////
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Threading.Tasks;

//using eDoxa.Games.TestHelper;
//using eDoxa.Games.TestHelper.Fixtures;
//using eDoxa.Games.TestHelper.Mocks;

//using Microsoft.AspNetCore.Mvc;

//using Xunit;

//namespace eDoxa.Games.UnitTests.Areas.Tournaments.Controllers
//{
//    public sealed class TournamentGameMatchesControllerTest : UnitTest
//    {
//        public TournamentGameMatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
//        {
//        }

//        [Fact]
//        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            var tournamentGameMatchesController = new TournamentGameMatchesController();

//            var mockHttpContextAccessor = new MockHttpContextAccessor();

//            tournamentGameMatchesController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

//            // Act
//            var result = await tournamentGameMatchesController.GetAsync();

//            // Assert
//            result.Should().BeOfType<OkResult>();
//        }
//    }
//}
