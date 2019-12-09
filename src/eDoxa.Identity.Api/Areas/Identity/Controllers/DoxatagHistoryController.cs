﻿// Filename: DoxatagHistoryController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Requests;
using eDoxa.Identity.Responses;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/doxatag-history")]
    [ApiExplorerSettings(GroupName = "Doxatag History")]
    public sealed class DoxatagHistoryController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDoxatagService _doxatagService;
        private readonly IMapper _mapper;

        public DoxatagHistoryController(IUserService userService, IDoxatagService doxatagService, IMapper mapper)
        {
            _userService = userService;
            _doxatagService = doxatagService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's Doxatag history.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoxatagResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var doxatagHistory = await _doxatagService.FetchDoxatagHistoryAsync(user);

            if (!doxatagHistory.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<DoxatagResponse>>(doxatagHistory));
        }

        [HttpPost]
        [SwaggerOperation("Create new user's Doxatag.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] ChangeDoxatagRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _doxatagService.ChangeDoxatagAsync(user, request.Name);

            if (result.Succeeded)
            {
                return this.Ok("The user's Doxatag has been created.");
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
