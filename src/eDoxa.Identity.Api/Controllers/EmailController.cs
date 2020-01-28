﻿// Filename: EmailController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Identity.Domain.Services;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/email")]
    [ApiExplorerSettings(GroupName = "Email")]
    public sealed class EmailController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EmailController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's address book.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(EmailDto))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindEmailAsync()
        {
            var user = await _userService.GetUserAsync(User);

            return this.Ok(_mapper.Map<EmailDto>(user));
        }

        [HttpGet("confirm")]
        [SwaggerOperation("User's forgot password.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string? userId, [FromQuery] string? code)
        {
            if (userId != null && code != null)
            {
                var user = await _userService.FindByIdAsync(userId);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{userId}'.");
                }

                var result = await _userService.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
                }

                return this.Ok(_mapper.Map<EmailDto>(user));
            }

            return this.Ok();
        }
    }
}
