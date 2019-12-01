// Filename: AddressBookControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Requests;
using eDoxa.Identity.Responses;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class AddressBookControllerTest : UnitTest
    {
        public AddressBookControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task DeleteAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        AddressBook = new Collection<UserAddress>
        //        {
        //            new UserAddress
        //            {
        //                Id = Guid.NewGuid(),
        //                City = "Test",
        //                PostalCode = "Test",
        //                Country = Country.Canada,
        //                Line1 = "Test"
        //            }
        //        }
        //    };

        //    var mockUserManager = new Mock<IUserManager>();

        //    mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

        //    mockUserManager.Setup(userManager => userManager.RemoveAddressAsync(It.IsAny<User>(), It.IsAny<Guid>()))
        //        .ReturnsAsync(IdentityResult.Failed())
        //        .Verifiable();

        //    var controller = new AddressBookController(mockUserManager.Object, TestMapper);

        //    // Act
        //    var result = await controller.DeleteAsync(user.AddressBook.First().Id);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

        //    mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        //    mockUserManager.Verify(userManager => userManager.RemoveAddressAsync(It.IsAny<User>(), It.IsAny<Guid>()), Times.Once);
        //}

        [Fact]
        public async Task DeleteAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var address = new Address(
                UserId.FromGuid(user.Id),
                Country.Canada,
                "Line1",
                null,
                "City",
                "State",
                "PostalCode");

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(addressService => addressService.RemoveAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.DeleteAsync(address.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().Be(address.Id);

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.RemoveAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(addressService => addressService.GetAddressBookAsync(It.IsAny<User>()))
                .ReturnsAsync(new Collection<Address>())
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.GetAddressBookAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var address = new Address(
                UserId.FromGuid(user.Id),
                Country.Canada,
                "Line1",
                null,
                "City",
                "State",
                "PostalCode");

            var addressBook = new List<Address>
            {
                address
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(addressService => addressService.GetAddressBookAsync(It.IsAny<User>()))
                .ReturnsAsync(addressBook)
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<ICollection<AddressResponse>>(addressBook));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.GetAddressBookAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var address = new Address(
                UserId.FromGuid(user.Id),
                Country.Canada,
                "Line1",
                null,
                "City",
                "State",
                "PostalCode");

            var addressBook = new List<Address>
            {
                address
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(
                    addressService => addressService.AddAddressAsync(
                        It.IsAny<User>(),
                        It.IsAny<Country>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            var request = new CreateAddressRequest(
                Country.Canada.Name,
                "New",
                "New",
                "New",
                "New",
                "New");

            // Act
            var result = await controller.PostAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(
                addressService => addressService.AddAddressAsync(
                    It.IsAny<User>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        //[Fact]
        //public async Task PutAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var user = new User();

        //    var mockUserManager = new Mock<IUserManager>();

        //    mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

        //    mockUserManager.Setup(
        //            userManager => userManager.UpdateAddressAsync(
        //                It.IsAny<User>(),
        //                It.IsAny<Guid>(),
        //                It.IsAny<string>(),
        //                It.IsAny<string>(),
        //                It.IsAny<string>(),
        //                It.IsAny<string>(),
        //                It.IsAny<string>()))
        //        .ReturnsAsync(IdentityResult.Failed())
        //        .Verifiable();

        //    var controller = new AddressBookController(mockUserManager.Object, TestMapper);

        //    var request = new AddressPutRequest(
        //        "New",
        //        "New",
        //        "New",
        //        "New",
        //        "New");

        //    // Act
        //    var result = await controller.PutAsync(Guid.NewGuid(), request);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

        //    mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        //    mockUserManager.Verify(
        //        userManager => userManager.UpdateAddressAsync(
        //            It.IsAny<User>(),
        //            It.IsAny<Guid>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>()),
        //        Times.Once);
        //}

        [Fact]
        public async Task PutAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var address = new Address(
                UserId.FromGuid(user.Id),
                Country.Canada,
                "Line1",
                null,
                "City",
                "State",
                "PostalCode");

            var addressBook = new List<Address>
            {
                address
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(
                    addressService => addressService.UpdateAddressAsync(
                        It.IsAny<User>(),
                        It.IsAny<AddressId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            var request = new UpdateAddressRequest(
                "New",
                "New",
                "New",
                "New",
                "New");

            // Act
            var result = await controller.PutAsync(addressBook.First().Id, request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(
                addressService => addressService.UpdateAddressAsync(
                    It.IsAny<User>(),
                    It.IsAny<AddressId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
