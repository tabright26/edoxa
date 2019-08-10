// Filename: UserDoxatagController.cs
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
    [Route("api/doxatag")]
    [ApiExplorerSettings(GroupName = "Doxatag")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class DoxatagController : ControllerBase
    {
        private readonly ICustomUserManager _userManager;
        private readonly IMapper _mapper;

        public DoxatagController(ICustomUserManager userManager, IMapper mapper)
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

            var doxatag = await _userManager.GetDoxatagAsync(user);

            if (doxatag == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<DoxatagResponse>(doxatag));
        }
    }
}