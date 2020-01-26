﻿// Filename: RegisterController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/register")]
    [ApiExplorerSettings(GroupName = "Register")]
    public sealed class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public RegisterController(IUserService userService, IServiceBusPublisher serviceBusPublisher)
        {
            _userService = userService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var result = await _userService.CreateAsync(
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    UserName = request.Email,
                    Country = Country.Canada
                },
                request.Password);

            if (result.Succeeded)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                await _serviceBusPublisher.PublishUserCreatedIntegrationEventAsync(user);

                var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

                await _serviceBusPublisher.PublishUserEmailConfirmationTokenGeneratedIntegrationEventAsync(user.Id.ConvertTo<UserId>(), code);

                //await _signInService.SignInAsync(user, false);

                return this.Ok();
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
