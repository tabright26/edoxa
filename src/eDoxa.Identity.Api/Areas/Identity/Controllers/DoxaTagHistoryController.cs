// Filename: DoxaTagHistoryController.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/doxatag-history")]
    [ApiExplorerSettings(GroupName = "DoxaTag History")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class DoxaTagHistoryController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public DoxaTagHistoryController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's DoxaTag history.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDoxaTagResponse>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var doxaTagHistory = await _userManager.GetDoxaTagHistoryAsync(user);

            if (!doxaTagHistory.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<UserDoxaTagResponse>>(doxaTagHistory));
        }

        /// <summary>
        ///     Create new user's DoxaTag.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        public async Task<IActionResult> PostAsync([FromBody] DoxaTagPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.SetDoxaTagAsync(user, request.Name);

                if (result.Succeeded)
                {
                    return this.Ok("The user's DoxaTag has been created.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
