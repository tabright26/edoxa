// Filename: DoxaTagController.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/doxa-tag")]
    [ApiExplorerSettings(GroupName = "DoxaTag")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class DoxaTagController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public DoxaTagController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's Doxatag.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var doxaTag = await _userManager.GetDoxaTagAsync(user);

            if (doxaTag == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<DoxaTagResponse>(doxaTag));
        }

        /// <summary>
        ///     Update user's Doxatag.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] DoxaTagPutRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.SetDoxaTagAsync(user, request.Name);

                if (result.Succeeded)
                {
                    return this.Ok("The user's Doxatag has been updated.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
