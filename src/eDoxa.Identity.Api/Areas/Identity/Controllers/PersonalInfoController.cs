// Filename: PersonalInfoController.cs
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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/personal-info")]
    [ApiExplorerSettings(GroupName = "Personal Info")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class PersonalInfoController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public PersonalInfoController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's personal info.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var personalInfo = await _userManager.GetPersonalInfoAsync(user);

            if (personalInfo == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<UserPersonalInfoResponse>(personalInfo));
        }

        /// <summary>
        ///     Update user's profile information.
        /// </summary>
        [HttpPatch]
        public async Task<IActionResult> PatchAsync([FromBody] JsonPatchDocument<PersonalInfoPatchRequest> document)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var profile = await _userManager.GetPersonalInfoAsync(user);

                var request = _mapper.Map<PersonalInfoPatchRequest>(profile);

                document.ApplyTo(request, ModelState); // TODO: Add fluentvalidation.

                var result = await _userManager.SetPersonalInfoAsync(
                    user,
                    request.FirstName,
                    request.LastName,
                    request.Gender,
                    request.BirthDate
                );

                if (result.Succeeded)
                {
                    return this.Ok("The user's profile information has been updated.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
