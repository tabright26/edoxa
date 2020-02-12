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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.AddressService.Setup(
                    addressService => addressService.AddAddressAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<Country>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Address>.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(TestMock.UserService.Object, TestMock.AddressService.Object, TestMapper);

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

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.AddressService.Verify(
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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.AddressService.Setup(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()))
                .ReturnsAsync(new Collection<Address>())
                .Verifiable();

            var controller = new AddressBookController(TestMock.UserService.Object, TestMock.AddressService.Object, TestMapper);

            // Act
            var result = await controller.FetchAddressBookAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.AddressService.Verify(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()), Times.Once);
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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.AddressService.Setup(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>())).ReturnsAsync(addressBook).Verifiable();

            var controller = new AddressBookController(TestMock.UserService.Object, TestMock.AddressService.Object, TestMapper);

            // Act
            var result = await controller.FetchAddressBookAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<ICollection<AddressDto>>(addressBook));

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.AddressService.Verify(addressService => addressService.FetchAddressBookAsync(It.IsAny<User>()), Times.Once);
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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.AddressService.Setup(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()))
                .ReturnsAsync(address)
                .Verifiable();

            TestMock.AddressService.Setup(addressService => addressService.RemoveAddressAsync(It.IsAny<Address>()))
                .ReturnsAsync(DomainValidationResult<Address>.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(TestMock.UserService.Object, TestMock.AddressService.Object, TestMapper);

            // Act
            var result = await controller.RemoveAddressAsync(address.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.AddressService.Verify(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()), Times.Once);

            TestMock.AddressService.Verify(addressService => addressService.RemoveAddressAsync(It.IsAny<Address>()), Times.Once);
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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.AddressService.Setup(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()))
                .ReturnsAsync(address)
                .Verifiable();

            TestMock.AddressService.Setup(
                    addressService => addressService.UpdateAddressAsync(
                        It.IsAny<Address>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Address>.Succeeded(address))
                .Verifiable();

            var controller = new AddressBookController(TestMock.UserService.Object, TestMock.AddressService.Object, TestMapper);

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

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.AddressService.Verify(addressService => addressService.FindUserAddressAsync(It.IsAny<User>(), It.IsAny<AddressId>()), Times.Once);

            TestMock.AddressService.Verify(
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
