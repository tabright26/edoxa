// Filename: EmailConfirmController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/email/confirm")]
    [ApiExplorerSettings(GroupName = "Email")]
    public sealed class EmailConfirmController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EmailConfirmController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("User's forgot password.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync([FromQuery] string? userId, [FromQuery] string? code)
        {
            if (userId != null && code != null)
            {
                var user = await _userService.FindByIdAsync(userId);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{userId}'.");
                }

                // BUG: Quick fix. Must be refactored. Related to the encoding.
                code = code.Replace(" ", "+");

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
