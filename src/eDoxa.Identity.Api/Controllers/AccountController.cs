// Filename: AccountController.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/account")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly IEventService _eventService;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public AccountController(
            IUserService userService,
            ISignInService signInService,
            IEventService eventService,
            IIdentityServerInteractionService interactionService,
            IServiceBusPublisher serviceBusPublisher
        )
        {
            _userService = userService;
            _signInService = signInService;
            _eventService = eventService;
            _interactionService = interactionService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [HttpPost("login")]
        [SwaggerOperation("Login to an account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAccountAsync([FromBody] LoginAccountRequest request)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(request.ReturnUrl);

            if (context != null)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    var result = await _signInService.PasswordSignInAsync(
                        user,
                        request.Password,
                        request.RememberMe,
                        true);

                    if (result.Succeeded)
                    {
                        await _eventService.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));

                        return this.Ok(request.ReturnUrl);
                    }

                    const string errorMessage = "Invalid email or password";

                    await _eventService.RaiseAsync(new UserLoginFailureEvent(user.UserName, errorMessage));

                    ModelState.AddModelError(DomainValidationError.FailedPreconditionPropertyName, errorMessage);

                    return this.BadRequest(new ValidationProblemDetails(ModelState));
                }
            }

            return this.Unauthorized();
        }

        [HttpGet("logout")]
        [SwaggerOperation("Logout from an account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(LogoutTokenDto))]
        public async Task<IActionResult> LogoutAccountAsync([FromQuery] string? logoutId = null)
        {
            var context = await _interactionService.GetLogoutContextAsync(logoutId);

            await _interactionService.RevokeTokensForCurrentSessionAsync();

            if (User?.Identity.IsAuthenticated ?? false)
            {
                await _signInService.SignOutAsync();

                await _eventService.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            var token = new LogoutTokenDto
            {
                LogoutId = logoutId,
                ClientName = context?.ClientName ?? context?.ClientId,
                PostLogoutRedirectUri = context?.PostLogoutRedirectUri,
                SignOutIFrameUrl = context?.SignOutIFrameUrl,
                ShowSignoutPrompt = context?.ShowSignoutPrompt ?? false,
                AutomaticRedirectAfterSignOut = true
            };

            return this.Ok(token);
        }

        [HttpPost("register")]
        [SwaggerOperation("Register an account.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> RegisterAccountAsync([FromBody] RegisterAccountRequest request)
        {
            var result = await _userService.CreateAsync(
                new User
                {
                    Id = new UserId(),
                    Email = request.Email,
                    Country = request.Country.ToEnumeration<Country>(),
                    Dob = new UserDob(DateTime.Parse(request.Dob))
                },
                request.Password);

            if (result.Succeeded)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                await _serviceBusPublisher.PublishUserCreatedIntegrationEventAsync(user, request.Ip);

                var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

                await _serviceBusPublisher.PublishUserEmailConfirmationTokenGeneratedIntegrationEventAsync(user.Id.ConvertTo<UserId>(), code);

                return this.Ok();
            }

            ModelState.Bind(result); // FRANCIS: VALIDATION SHOULD BE MORE GENERAL FOR REGISTRATION

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
