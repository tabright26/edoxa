// Filename: AccountControllerTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Google.Protobuf;

using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class AccountControllerTest : UnitTest
    {
        public AccountControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        private const string TestEmail = "test@edoxa.gg";
        private const string TestPassword = "Pass@word1";

        private static User GenerateUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                AccessFailedCount = 0,
                ConcurrencyStamp = string.Empty,
                Country = Country.Canada,
                Dob = new UserDob(1995, 08, 04),
                Email = TestEmail,
                EmailConfirmed = true,
                LockoutEnd = null,
                LockoutEnabled = false,
                NormalizedEmail = TestEmail
            };
        }

        [Fact]
        public async Task LoginAccountAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            TestMock.InteractionService.Setup(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>()))
                .ReturnsAsync(new AuthorizationRequest())
                .Verifiable();

            TestMock.UserService.Setup(userService => userService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.SignInService.Setup(
                    signInService => signInService.PasswordSignInAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed)
                .Verifiable();

            TestMock.EventService.Setup(eventService => eventService.RaiseAsync(It.IsAny<Event>())).Verifiable();

            var controller = new AccountController(
                TestMock.UserService.Object,
                TestMock.SignInService.Object,
                TestMock.EventService.Object,
                TestMock.InteractionService.Object,
                TestMock.ServiceBusPublisher.Object);

            var request = new LoginAccountRequest
            {
                Email = TestEmail,
                Password = TestPassword,
                RememberMe = false,
                ReturnUrl = string.Empty
            };

            // Act
            var result = await controller.LoginAccountAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            TestMock.InteractionService.Verify(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            TestMock.SignInService.Verify(
                signInService => signInService.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);

            TestMock.EventService.Verify(eventService => eventService.RaiseAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task LoginAccountAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            const string returnUrl = "/";

            TestMock.InteractionService.Setup(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>()))
                .ReturnsAsync(new AuthorizationRequest())
                .Verifiable();

            TestMock.UserService.Setup(userService => userService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.SignInService.Setup(
                    signInService => signInService.PasswordSignInAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success)
                .Verifiable();

            TestMock.EventService.Setup(eventService => eventService.RaiseAsync(It.IsAny<Event>())).Verifiable();

            var controller = new AccountController(
                TestMock.UserService.Object,
                TestMock.SignInService.Object,
                TestMock.EventService.Object,
                TestMock.InteractionService.Object,
                TestMock.ServiceBusPublisher.Object);

            var request = new LoginAccountRequest
            {
                Email = TestEmail,
                Password = TestPassword,
                RememberMe = false,
                ReturnUrl = returnUrl
            };

            // Act
            var result = await controller.LoginAccountAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(returnUrl);
            TestMock.InteractionService.Verify(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            TestMock.SignInService.Verify(
                signInService => signInService.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                Times.Once);

            TestMock.EventService.Verify(eventService => eventService.RaiseAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task LoginAccountAsync_ShouldBeUnauthorizedResult()
        {
            // Arrange
            TestMock.InteractionService.Setup(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>())).Verifiable();

            var controller = new AccountController(
                TestMock.UserService.Object,
                TestMock.SignInService.Object,
                TestMock.EventService.Object,
                TestMock.InteractionService.Object,
                TestMock.ServiceBusPublisher.Object);

            var request = new LoginAccountRequest
            {
                Email = TestEmail,
                Password = TestPassword,
                RememberMe = false,
                ReturnUrl = string.Empty
            };

            // Act
            var result = await controller.LoginAccountAsync(request);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
            TestMock.InteractionService.Verify(interactionService => interactionService.GetAuthorizationContextAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task LogoutAccountAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var logoutRequest = new LogoutRequest(string.Empty, new LogoutMessage(new ValidatedEndSessionRequest()));

            TestMock.InteractionService.Setup(interactionService => interactionService.GetLogoutContextAsync(It.IsAny<string>()))
                .ReturnsAsync(logoutRequest)
                .Verifiable();

            TestMock.InteractionService.Setup(interactionService => interactionService.RevokeTokensForCurrentSessionAsync()).Verifiable();

            var controller = new AccountController(
                TestMock.UserService.Object,
                TestMock.SignInService.Object,
                TestMock.EventService.Object,
                TestMock.InteractionService.Object,
                TestMock.ServiceBusPublisher.Object);

            // Act
            var result = await controller.LogoutAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeOfType<LogoutTokenDto>();
            TestMock.InteractionService.Verify(interactionService => interactionService.GetLogoutContextAsync(It.IsAny<string>()), Times.Once);
            TestMock.InteractionService.Verify(interactionService => interactionService.RevokeTokensForCurrentSessionAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterAccountAsync_ShouldBeOkResult()
        {
            // Arrange
            const string token = "123ABC";

            TestMock.UserService.Setup(userService => userService.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            TestMock.UserService.Setup(userService => userService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.ServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<IMessage>())).Verifiable();

            TestMock.UserService.Setup(userService => userService.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync(token).Verifiable();

            var controller = new AccountController(
                TestMock.UserService.Object,
                TestMock.SignInService.Object,
                TestMock.EventService.Object,
                TestMock.InteractionService.Object,
                TestMock.ServiceBusPublisher.Object);

            var request = new RegisterAccountRequest
            {
                Email = TestEmail,
                Password = TestPassword,
                Country = EnumCountryIsoCode.CA,
                Dob = "08/04/1995",
                Ip = string.Empty
            };

            // Act
            var result = await controller.RegisterAccountAsync(request);

            // Assert
            result.Should().BeOfType<OkResult>();
            TestMock.UserService.Verify(userService => userService.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()), Times.Once);
            TestMock.ServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<IMessage>()), Times.Exactly(2));
        }
    }
}
