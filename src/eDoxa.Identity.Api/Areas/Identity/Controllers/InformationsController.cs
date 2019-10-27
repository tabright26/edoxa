﻿// Filename: InformationsController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/informations")]
    [ApiExplorerSettings(GroupName = "Informations")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class InformationsController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public InformationsController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's personal info.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserInformationsResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var informations = await _userManager.GetInformationsAsync(user);

            if (informations == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<UserInformationsResponse>(informations));
        }

        /// <summary>
        ///     Create user's profile information.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] InformationsPostRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var informations = await _userManager.GetInformationsAsync(user);

            if (informations != null)
            {
                return this.BadRequest("The user's personal information has already been created.");
            }

            var result = await _userManager.CreateInformationsAsync(
                user,
                request.FirstName,
                request.LastName,
                request.Gender,
                new Dob(request.Dob.Year, request.Dob.Month, request.Dob.Day));

            if (result.Succeeded)
            {
                return this.Ok("The user's personal info has been created.");
            }

            ModelState.Bind(result);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Update user's profile information.
        /// </summary>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync([FromBody] InformationsPutRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var informations = await _userManager.GetInformationsAsync(user);

            if (informations == null)
            {
                return this.BadRequest("The user's personal informations does not exist.");
            }

            var result = await _userManager.UpdateInformationsAsync(user, request.FirstName);

            if (result.Succeeded)
            {
                return this.Ok("The user's personal info has been updated.");
            }

            ModelState.Bind(result);

            return this.ValidationProblem(ModelState);
        }
    }
}