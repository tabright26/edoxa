// Filename: UserDoxatagController.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/doxa-tag")]
    [ApiExplorerSettings(GroupName = "Users")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class UserDoxaTagController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UserDoxaTagController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find Doxatag by user id.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return this.NotFound("User's not found.");
            }

            var doxaTag = await _userManager.GetDoxaTagAsync(user);

            if (doxaTag == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<DoxaTagResponse>(doxaTag));
        }
    }
}
