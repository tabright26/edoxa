// Filename: UsersControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Area.User.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        //[TestMethod]
        //public async Task FindUsersAsync_ShouldBeOkObjectResult()
        //{
        //    // Arrange
        //    _mockUserQueries.Setup(service => service.FetchUsersAsync())
        //        .ReturnsAsync(
        //            new List<Domain.AggregateModels.UserAggregate.User>
        //            {
        //                new Domain.AggregateModels.UserAggregate.User(new Gamertag("Gamertag"), new Email("Address"), new BirthDate(DateTime.UnixEpoch), new PersonalName("FirstName", "LastName"))
        //            }
        //        )
        //        .Verifiable();

        //    var controller = new UsersController(_mockUserQueries.Object, MapperExtensions.Mapper);

        //    // Act
        //    var result = await controller.FindUsersAsync();

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>();
        //}

        //[TestMethod]
        //public async Task FindUsersAsync_ShouldBeNoContentObjectResult()
        //{
        //    // Arrange
        //    _mockUserQueries.Setup(queries => queries.FetchUsersAsync()).ReturnsAsync(new List<Domain.AggregateModels.UserAggregate.User>()).Verifiable();

        //    var controller = new UsersController(_mockUserQueries.Object, MapperExtensions.Mapper);

        //    // Act
        //    var result = await controller.FindUsersAsync();

        //    // Assert
        //    result.Should().BeOfType<NoContentResult>();
        //}
    }
}
