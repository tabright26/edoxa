// Filename: UserProfileController.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/users/{userId}")]
    [ApiExplorerSettings(GroupName = "Profile")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly ICustomUserManager _userManager;
        private readonly IMapper _mapper;

        public ProfileController(ICustomUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's profile.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var doxatag = await _userManager.GetProfileAsync(user);

            if (doxatag == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<ProfileResponse>(doxatag));
        }
    }
}