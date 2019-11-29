﻿// Filename: PhoneController.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Requests;
using eDoxa.Identity.Responses;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/phone")]
    [ApiExplorerSettings(GroupName = "Phone")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public sealed class PhoneController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PhoneController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's phone.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PhoneResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            if (phoneNumber == null)
            {
                return this.NotFound("Phone number not found.");
            }

            return this.Ok(_mapper.Map<PhoneResponse>(user));
        }

        [HttpPost]
        [SwaggerOperation("Udpate user's phone.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PhoneResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] ChangePhoneRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _userService.SetPhoneNumberAsync(user, request.Number);

            if (result.Succeeded)
            {
                return this.Ok(_mapper.Map<PhoneResponse>(user));
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
