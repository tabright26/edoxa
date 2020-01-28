// Filename: AddressBookControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class AddressBookControllerTest : UnitTest
    {
        public AddressBookControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task AddAddressAsync_ShouldBeOkObjectResult()
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

            mockAddressService.Setup(
                    addressService => addressService.AddAddressAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<Country>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            var request = new CreateAddressRequest
            {
                CountryIsoCode = EnumCountryIsoCode.CA,
                Line1 = "1234 Test Street",
                Line2 = null,
                City = "Toronto",
                State = "Ontario",
                PostalCode = "A1A1A1"
            };

            // Act
            var result = await controller.AddAddressAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(
                addressService => addressService.AddAddressAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task FetchAddressBookAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockAddressService = new Mock<IAddressService>();

            mockAddressService.Setup(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()))
                .ReturnsAsync(new Collection<Address>())
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.FetchAddressBookAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FetchAddressBookAsync_ShouldBeOkObjectResult()
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

            mockAddressService.Setup(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>())).ReturnsAsync(addressBook).Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.FetchAddressBookAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<ICollection<AddressDto>>(addressBook));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task RemoveAddressAsync_ShouldBeOkObjectResult()
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

            mockAddressService.Setup(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()))
                .ReturnsAsync(address)
                .Verifiable();

            mockAddressService.Setup(addressService => addressService.RemoveAddressAsync(It.IsAny<Address>()))
                .ReturnsAsync(DomainValidationResult.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            // Act
            var result = await controller.RemoveAddressAsync(address.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.RemoveAddressAsync(It.IsAny<Address>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAddressAsync_ShouldBeOkObjectResult()
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

            mockAddressService.Setup(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()))
                .ReturnsAsync(address)
                .Verifiable();

            mockAddressService.Setup(
                    addressService => addressService.UpdateAddressAsync(
                        It.IsAny<Address>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(mockUserManager.Object, mockAddressService.Object, TestMapper);

            var request = new UpdateAddressRequest
            {
                Line1 = "1234 Test Street",
                Line2 = null,
                City = "Toronto",
                State = "Ontario",
                PostalCode = "A1A1A1"
            };

            // Act
            var result = await controller.UpdateAddressAsync(address.Id, request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockAddressService.Verify(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()), Times.Once);

            mockAddressService.Verify(
                addressService => addressService.UpdateAddressAsync(
                    It.IsAny<Address>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
